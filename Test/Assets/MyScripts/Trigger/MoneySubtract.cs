using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneySubtract : MonoBehaviour
{
        [SerializeField] private GameObject moneySubtractPrefab;
        [SerializeField] private Transform canvasParent;
        
        [SerializeField] private TextMeshProUGUI _totalMoneyText;
        [SerializeField] private Image _imageSlider;
        [SerializeField] private Player _player;
        
        private PlayerVisual _playerVisual;
        
        private int maxMoney = 140;
        private GameObject _activePopup;
        public Coroutine hideCoroutine;                    
        public int _currentMoneyAmount;

        public float Timer;
        private float _resetTime = 1;
        
        private void Start()
        {
            _playerVisual = GetComponent<PlayerVisual>();
            UpdateSlider();
        }

        private void UpdateSlider()
        {
            if (_player.Money <= 0)
            {
                _playerVisual.BadVisualChanger();
            }
            
            _imageSlider.fillAmount = (float)_player.Money / maxMoney;
        }
        
        
        private void InteractWithObject()
        {
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
                _activePopup = Instantiate(moneySubtractPrefab, canvasParent);
                RectTransform rectTransform = _activePopup.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = Vector2.zero;
            }
        }
        
        
        private void ShowMoneyPopup(int amount)
        {
            
            InteractWithObject();
            UpdatePopupText(amount);
        }
        
        private void UpdatePopupText(int amount)
        {
            
            _currentMoneyAmount += amount;
            _player.Money  -= amount;
             UpdateSlider();
            _totalMoneyText.text = _player.Money.ToString();
            _activePopup.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = _currentMoneyAmount.ToString();
        }
        
        
        public void RemoveMoney(int amount)
        {
            ShowMoneyPopup(amount);
        }
        
       
        
        
        private IEnumerator HideAfterDelay()
        {
            yield return new WaitForSeconds(2);
            Timer = 0f;
            
            while (Timer < _resetTime)
            {
                Timer += Time.deltaTime;
                Debug.Log("TImer" + Timer);
                yield return null;
            }

            if (moneySubtractPrefab != null)
            {
                Destroy(_activePopup);
                _activePopup = null;
            }
           
            
            
            Destroy(_activePopup);
            _currentMoneyAmount = 0;
            yield return null;
        }
        
}

