using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Trigger
{
    public class MoneyPopup : MonoBehaviour
    {
        [SerializeField] private GameObject moneyPopupPrefab;
        [SerializeField] private Transform canvasParent;
        
        [SerializeField] private TextMeshProUGUI _totalMoneyText;
        [SerializeField] private Image _imageSlider;
        [SerializeField] private Player _player;
        private PlayerVisual _playerVisual;
        
        public int maxMoney;
        private GameObject _activePopup;
        public Coroutine hideCoroutine;                    
        public int _currentMoneyAmount;
        
        public float Timer;
        private float _resetTime = 1;
        
        private void Start()
        {
            maxMoney = 40;
            _playerVisual = GetComponent<PlayerVisual>();
            UpdateSlider();
        }
        
        
        private void UpdateSlider()
        {
            if (_player.Money >= maxMoney)
            {
                Debug.Log("UPDATEEE");
                _playerVisual.GoodVisualChanger();
               
            }
            
            _imageSlider.fillAmount = (float)_player.Money / maxMoney;
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
            _player.Money += amount;
             UpdateSlider();
            _totalMoneyText.text = _player.Money.ToString();
            _activePopup.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = _currentMoneyAmount.ToString();
        }
        
        
        public void AddMoney(int amount)
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
                yield return null;
            }

            if (moneyPopupPrefab != null)
            {
                Destroy(_activePopup);
                _activePopup = null;
            }
           
            
            
            Destroy(_activePopup);
            _currentMoneyAmount = 0;
            yield return null;
        }
        
    }
}
