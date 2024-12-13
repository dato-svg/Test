using System.Collections.Generic;
using TMPro;
using Trigger;
using UnityEngine;


public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private List<SkinnedMeshRenderer> _visual;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TextMeshProUGUI _stateText;
    private string[] _text = new[] {"poor", "wealthy", "rich"};
    private MoneyPopup _money;
    private int _currentIndexText;
    private int _currentIndex;

    private void Start()
    {
        _money = GetComponent<MoneyPopup>();
        _currentIndexText = 0;
        _currentIndex = 0;
        foreach (var v in _visual)
        {
            v.enabled = false;
        }
        Debug.Log("Enabled");
        _visual[_currentIndex].enabled = true;
        _stateText.text = _text[_currentIndexText];
    }
    
    public void GoodVisualChanger()
    {
       
        _currentIndex++;
        _currentIndexText++;
        foreach (var v in _visual)
        {
            v.enabled = false;
        }
        _visual[_currentIndex].enabled = true;
        _stateText.text = _text[_currentIndexText];
        _money.maxMoney *= 2;
    }
    
    
    public void BadVisualChanger()
    {
        if (_currentIndex > 0)
        {
            _currentIndex --;
            _currentIndexText--;
        }
        else
        {
            Debug.Log("Game Over");
            _gameManager.EnablePanel(4);
            gameObject.SetActive(false);
        }
        
        foreach (var v in _visual)
        {
            v.enabled = false;
        }
        _visual[_currentIndex].enabled = true;
        _stateText.text = _text[_currentIndexText];
    }
}

