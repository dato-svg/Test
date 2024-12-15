using System;
using System.Collections.Generic;
using PlayerControl;
using TMPro;
using Trigger;
using UnityEngine;


public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private List<SkinnedMeshRenderer> _visual;
    [SerializeField] private Animator _gameOverAnimator;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TextMeshProUGUI _stateText;
    [SerializeField] private ObjectRotator _player;
    
    
    private string[] _text = new[] {"Бедный", "Состоятельный", "Богатый"};
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
        _player.StartRotation();
        _currentIndex++;
        _currentIndexText++;
        
        if (_currentIndex == 2)
        {
            _money.FullSlider.SetActive(false);
            _money.maxMoney = Int32.MaxValue;
            
            foreach (var v in _visual)
            {
                v.enabled = false;
            }
            
            _visual[_currentIndex].enabled = true;
            _stateText.text = _text[_currentIndexText];
            
            return;
        }
        
        
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
        Debug.Log($"BadVisualChanger called. Current index: {_currentIndex}, Money: {_gameManager._player.GetComponent<PlayerUI>().Money}");
        if (_currentIndex > 0)
        {
            _currentIndex --;
            _currentIndexText--;
        }
        else
        {
            Debug.Log("Game Over");
           _gameManager.EnablePanel(4);
           _gameOverAnimator.SetTrigger("Loose");
           gameObject.GetComponent<PlayerMovement>().enabled = false;
           gameObject.GetComponent<Animator>().SetTrigger("Defeat");
           gameObject.GetComponent<SpawnObjectWithSound>().SpawnObject();
        }
        
        foreach (var v in _visual)
        {
            v.enabled = false;
        }
        _visual[_currentIndex].enabled = true;
        _stateText.text = _text[_currentIndexText];
    }
}

