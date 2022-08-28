using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyManager : MonoBehaviour
{
    [SerializeField] private int totalMoney=0;
    public int publicTotalMoney
    {
        get { return totalMoney; }
        set { totalMoney = value; }
    }

    private void OnEnable()
    {
        TriggerEventManager.OnMoneyCollected += IncreaseMoney;
        TriggerEventManager.OnBuyShopAndFarmer += BuyArea;
    }
    private void OnDisable()
    {
        TriggerEventManager.OnMoneyCollected -= IncreaseMoney;
        TriggerEventManager.OnBuyShopAndFarmer -= BuyArea;
    }
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
    void BuyArea()
    {
        if (TriggerEventManager.buyArea != null)
        {
            if (totalMoney >= 1)
            {
                TriggerEventManager.buyArea.Buy(1);
                totalMoney -= 1;
            }
        }
    }
    private void IncreaseMoney()
    {
        if (TriggerEventManager.shopManager.moneyList.Count > 0)
        {
            totalMoney += 5;    // Para de�erini artt�r
            TriggerEventManager.shopManager.RemoveLastMoney();  // Her para kazan�ld���nda money listesindeki paralar ��kar�l�r
        }        
    }
}
