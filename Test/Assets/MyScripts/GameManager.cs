using System.Collections.Generic;
using PlayerControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject _player;
    public MoneyMultiplierUI MoneyMultiplierUi;
    
    [SerializeField] private List<GameObject> _allPanels;
    [SerializeField] private KeyCode keyToPress = KeyCode.Space; 
    [SerializeField] private bool useDebugLog = true;

    private void Start()
    {
        MoneyMultiplierUi = GameObject.Find("MainCanvas").GetComponent<MoneyMultiplierUI>();
        EnablePanel(0); 
        _player.GetComponent<Animator>().SetBool("Move",false);
        _player.GetComponent<PlayerMovement>().enabled = false;
    }

    private void Update()
    {
      
        if (Input.GetKeyDown(keyToPress))
        {
            StartGame();
        }

    
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        if (useDebugLog)
        {
            useDebugLog = false;
            EnablePanel(1); 
            _player.GetComponent<PlayerMovement>().enabled = true;
            _player.GetComponent<Animator>().SetBool("Move",true);
            
        }
    }

    public void EnablePanel(int index)
    {
        foreach (var p in _allPanels)
        {
            p.SetActive(false);
        }
        _allPanels[index].SetActive(true);
    }

    private void DisableAllPanels()
    {
        foreach (var p in _allPanels)
        {
            p.SetActive(false);
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}