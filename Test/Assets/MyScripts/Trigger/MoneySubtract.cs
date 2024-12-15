using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneySubtract : MonoBehaviour
{
    [SerializeField] private GameObject moneySubtractPrefab;
    [SerializeField] private Transform canvasParent;
    [SerializeField] private GameObject _redParticle;
    [SerializeField] private TextMeshProUGUI _totalMoneyText;
    [SerializeField] private Image _imageSlider;
    [SerializeField] private PlayerUI _playerUi;
    
    private PlayerVisual _playerVisual;
    
    private int maxMoney = 140;
    private GameObject _activePopup;
    public Coroutine hideCoroutine;                    
    public int _currentMoneyAmount;

    public float Timer;
    private float _resetTime = 0.05f;

    private Animator _popupAnimator;
    
    private void Start()
    {
        _playerVisual = GetComponent<PlayerVisual>();
        UpdateSlider();
    }
    
    private void UpdateSlider()
    {
        if (_playerUi.Money <= 0)
        {
            _playerVisual.BadVisualChanger();
        }
        
        _imageSlider.fillAmount = (float)_playerUi.Money / maxMoney;
    }
    
    private void InteractWithObject()
    {
        UpdateSlider();
        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }

        hideCoroutine = StartCoroutine(HideAfterDelay());

        if (_activePopup == null)
        {
            if (moneySubtractPrefab == null || canvasParent == null)
            {
                return;
            }

            _activePopup = Instantiate(moneySubtractPrefab, canvasParent);
            RectTransform rectTransform = _activePopup.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = Vector2.zero;
            }
            
            _popupAnimator = _activePopup.GetComponent<Animator>();
        }
        UpdateSlider();
    }

    private void ShowMoneyPopup(int amount)
    {
        InteractWithObject();
        UpdatePopupText(amount);
        UpdateSlider();
    }

    private void UpdatePopupText(int amount)
    {
        UpdateSlider();
        if (_activePopup == null)
        {
            return;
        }

        Transform textTransform = _activePopup.transform.GetChild(0).GetChild(0);
        if (textTransform == null)
        {
            return;
        }

        TextMeshProUGUI textComponent = textTransform.GetComponent<TextMeshProUGUI>();
        if (textComponent == null)
        {
            return;
        }
        
        var particleSystem = _redParticle.GetComponent<ParticleSystem>();
        particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        particleSystem.Play();
        
        _currentMoneyAmount += amount;
        _playerUi.Money -= amount;
        UpdateSlider();
        _totalMoneyText.text = _playerUi.Money.ToString();
        textComponent.text = _currentMoneyAmount.ToString();
    }

    public void RemoveMoney(int amount)
    {
        ShowMoneyPopup(amount);
        UpdateSlider();
    }
    
    private IEnumerator HideAfterDelay()
    {
        Timer = 0f;
        
        while (Timer < _resetTime)
        {
            Timer += Time.deltaTime;
            yield return null;
        }

      
        if (_popupAnimator != null)
        {
            _popupAnimator.SetTrigger("Start");
            
            yield return new WaitForSeconds(0.5f); 
        }

        if (_activePopup != null)
        {
            Destroy(_activePopup);
            _activePopup = null;
        }

        _currentMoneyAmount = 0;
    }
}
