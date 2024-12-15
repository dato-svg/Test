using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public int Money;
    [HideInInspector] public int Multiplier;

    public TextMeshProUGUI PlayerTakeMoneyText;
    public TextMeshProUGUI CanvasTakeMoneyText;
    public TextMeshProUGUI PlayerTakeRewardMoney;
    
    
    
    private void Awake()
    {
        Money = 20;
        Multiplier = 1;
    }

    
  
}