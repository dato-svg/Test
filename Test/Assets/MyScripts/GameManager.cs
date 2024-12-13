using System.Collections.Generic;
using PlayerControl;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
    {
        public GameObject _player;

        [SerializeField] private List<GameObject> _allPanels;
        
        [SerializeField] private string keyToPress = "space"; 
        [SerializeField] private bool useDebugLog = true;

        private void Start()
        {
            EnablePanel(0);
            _player.SetActive(false);
        }
        
        
        private void Update()
        {
            
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                PerformAction();
                _player.SetActive(true);
            }
        }

        
        private void PerformAction()
        {
            if (useDebugLog)
            {
                useDebugLog = false;
                EnablePanel(1);
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

