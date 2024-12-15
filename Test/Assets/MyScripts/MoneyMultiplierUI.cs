using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyMultiplierUI : MonoBehaviour
{
    public SceneLoader SceneLoader;
    
    [SerializeField] private RectTransform arrow;
    [SerializeField] private TextMeshProUGUI potentialMoneyText;
    [SerializeField] private TextMeshProUGUI potentialXMoneyText;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Button _rewardButton;
  
    
    [SerializeField] private float minAngle = -72.7f;
    [SerializeField] private float maxAngle = 72.7f;
    [SerializeField] private float minDuration = 2f; 
    [SerializeField] private float maxDuration = 2.5f;
    [SerializeField] private float spinSpeedMin;
    [SerializeField] private float spinSpeedMax;

    private int _currentMoney;
    
    private void Start()
    {
        _rewardButton.enabled = false;
        SceneLoader = GetComponent<SceneLoader>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void CoroutineActivate()
    {
        StartCoroutine(SpinArrow());
    }

    private IEnumerator SpinArrow()
    {
        float spinDuration = Random.Range(minDuration, maxDuration);
        float elapsedTime = 0f;

        float speed = Random.Range(spinSpeedMin, spinSpeedMax);
        float startAngle = minAngle;

        while (elapsedTime < spinDuration)
        {
            elapsedTime += Time.deltaTime;
    
            float t = elapsedTime / spinDuration;
            float currentAngle = Mathf.Lerp(minAngle, maxAngle, Mathf.PingPong(elapsedTime * speed / 1000f, 1f));
            arrow.localRotation = Quaternion.Euler(0, 0, currentAngle);
            CheckHit();
            yield return null;
        }
        
        _rewardButton.enabled = true;
        CheckHit();
    }

    private void CheckHit()
    {
        float currentZAngle = Mathf.Repeat(arrow.localRotation.eulerAngles.z, 360f);
        if (currentZAngle > 180f) currentZAngle -= 360f;
        
        if (currentZAngle >= 35f && currentZAngle <= 90f)
        {
            Debug.Log("2");
            _currentMoney = _gameManager._player.GetComponent<PlayerUI>().Money;
            _currentMoney *= 2;
            potentialMoneyText.text =  _currentMoney.ToString();
            potentialXMoneyText.text = "2".ToString();
        }
        
        if (currentZAngle >= -11.81f && currentZAngle <= 35f)
        {
            Debug.Log("3");
            _currentMoney = _gameManager._player.GetComponent<PlayerUI>().Money;
            _currentMoney *= 3;
            potentialMoneyText.text =  _currentMoney.ToString();
            potentialXMoneyText.text = "3".ToString();
        }
    
        if (currentZAngle >= -58.5f && currentZAngle <= -11.81f)
        {
            Debug.Log("4");
            _currentMoney = _gameManager._player.GetComponent<PlayerUI>().Money;
            _currentMoney *= 4;
            potentialMoneyText.text =  _currentMoney.ToString();
            potentialXMoneyText.text = "4".ToString();
        }
    
        if (currentZAngle >= -90.93f && currentZAngle <= -58.5f)
        {
            Debug.Log("5");
            _currentMoney = _gameManager._player.GetComponent<PlayerUI>().Money;
            _currentMoney *= 5;
            potentialMoneyText.text =  _currentMoney.ToString();
            potentialXMoneyText.text = "5".ToString();
        }
    }

    public void ActiveRewardCoroutine()
    {
        StartCoroutine(TakeRewardMoney());
    }

    private IEnumerator TakeRewardMoney()
    {
        int startMoney = 0;
        int targetMoney = _currentMoney;
        float duration = 1f;
        float elapsedTime = 0f;

        TextMeshProUGUI canvasMoneyText = _gameManager._player.GetComponent<PlayerUI>().CanvasTakeMoneyText;

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
        SceneLoader.NextSceneActive();
    }

    
}
