using System.Collections;
using PlayerControl;
using TMPro;
using UnityEngine;

public class OverController : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerUI>() != null)
        {
            other.GetComponent<Animator>().SetTrigger("Win");
            _gameManager._player.GetComponent<PlayerMovement>().enabled = false;
            _gameManager.EnablePanel(2);
            _gameManager.MoneyMultiplierUi.CoroutineActivate();
            ShowAllMoney();
        }
    }
    
    public void TakeAllMoney()
    {
        StartCoroutine(MoneyTake());
    }
    
    private void ShowAllMoney()
    {
        var player = _gameManager._player.GetComponent<PlayerUI>();
        player.PlayerTakeMoneyText.text = player.Money.ToString();
    }
    
    
    private IEnumerator MoneyTake()
    {
        var player = _gameManager._player.GetComponent<PlayerUI>();
        TextMeshProUGUI canvasMoneyText = player.CanvasTakeMoneyText;

        int startMoney = 0;
        int targetMoney = player.Money;
        float duration = 1f; 
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            int currentMoney = Mathf.RoundToInt(Mathf.Lerp(startMoney, targetMoney, elapsedTime / duration));
            canvasMoneyText.text = currentMoney.ToString();
            yield return null; 
        }
        
        canvasMoneyText.text = targetMoney.ToString();

        yield return new WaitForSeconds(0.4f);
        
        _gameManager.EnablePanel(3);
        _gameManager.MoneyMultiplierUi.SceneLoader.NextSceneActive();
    }

}
