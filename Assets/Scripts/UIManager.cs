using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Space]
    [Header("Money Controller")]
    [SerializeField] BuyManager buyManager;
    [SerializeField] private TextMeshProUGUI totalMoneyTxt;
    private void OnEnable()
    {
        TriggerEventManager.OnMoneyCollected += IncreaseMoneyUI;
        TriggerEventManager.OnBuyShopAndFarmer += DecreaseMoneyUI;
    }
    private void OnDisable()
    {
        TriggerEventManager.OnMoneyCollected -= IncreaseMoneyUI;
        TriggerEventManager.OnBuyShopAndFarmer -= DecreaseMoneyUI;
    }
    void Start()
    {
        totalMoneyTxt.text = "$ " + buyManager.publicTotalMoney.ToString();
    }
    void Update()
    {
        
    }
    private void IncreaseMoneyUI()
    {
        // UI Money para arttýr
        totalMoneyTxt.text = "$ " + buyManager.publicTotalMoney.ToString();
    }
    private void DecreaseMoneyUI()
    {
        // UI Money para azalt
        totalMoneyTxt.text = "$ " + buyManager.publicTotalMoney.ToString();
    }
}
