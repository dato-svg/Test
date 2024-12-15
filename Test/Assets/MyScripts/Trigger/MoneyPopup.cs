using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace Trigger
{
    public class MoneyPopup : MonoBehaviour
    {
        public GameObject FullSlider;
        public int maxMoney;
        
        [SerializeField] private GameObject moneyPopupPrefab;
        [SerializeField] private Transform canvasParent;
        
        [SerializeField] private TextMeshProUGUI _totalMoneyText;
        [SerializeField] private Image _imageSlider;
      
        [SerializeField] private PlayerUI _playerUi;
        [SerializeField] private GameObject _greenParticle;
        
        private PlayerVisual _playerVisual;
        
        private GameObject _activePopup;
        private Coroutine hideCoroutine;
        private int _currentMoneyAmount;
        
        public float Timer;
        private float _resetTime = 0.05f;

        private Animator _popupAnimator;

        private void Start()
        {
            FullSlider = GameObject.Find("CanvasPlayer");
            maxMoney = 50;
            _playerVisual = GetComponent<PlayerVisual>();
            UpdateSlider();
        }
        
        private void UpdateSlider()
        {
            if (_playerUi.Money >= maxMoney)
            {
                Debug.Log("UPDATE");
                _playerVisual.GoodVisualChanger();
            }
            
            _imageSlider.fillAmount = (float)_playerUi.Money / maxMoney;
        }

        private void InteractWithObject()
        {
            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine); 
            }
            
            hideCoroutine = StartCoroutine(HideAfterDelay());
            if (_activePopup == null)
            {
                _activePopup = Instantiate(moneyPopupPrefab, canvasParent);
                RectTransform rectTransform = _activePopup.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = Vector2.zero;

                
                _popupAnimator = _activePopup.GetComponent<Animator>();
            }
        }
        
        private void ShowMoneyPopup(int amount)
        {
            InteractWithObject();
            StartCoroutine(AnimateMoneyPopup(amount));
          
            
            var particleSystem = _greenParticle.GetComponent<ParticleSystem>();
            particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            particleSystem.Play();
        }

        private IEnumerator AnimateMoneyPopup(int amount)
        {
            
            
            int startMoney = _playerUi.Money;
            int targetMoney = startMoney + amount;
            int startPopupMoney = _currentMoneyAmount;
            int targetPopupMoney = startPopupMoney + amount;

            float duration = 0.5f;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;

                int currentMoney = Mathf.RoundToInt(Mathf.Lerp(startMoney, targetMoney, t));
                int currentPopupMoney = Mathf.RoundToInt(Mathf.Lerp(startPopupMoney, targetPopupMoney, t));

                _playerUi.Money = currentMoney;
                _currentMoneyAmount = currentPopupMoney;

                UpdateSlider();
                
                _totalMoneyText.text = _playerUi.Money.ToString();
                if (_activePopup != null)
                {
                    _activePopup.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = _currentMoneyAmount.ToString();
                }

                yield return null;
            }

            _playerUi.Money = targetMoney;
            _currentMoneyAmount = targetPopupMoney;

            UpdateSlider();

            _totalMoneyText.text = _playerUi.Money.ToString();
            if (_activePopup != null)
            {
                _activePopup.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = _currentMoneyAmount.ToString();
            }
        }

        public void AddMoney(int amount)
        {
            ShowMoneyPopup(amount);
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
}
