using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyManager : MonoBehaviour
{
    [SerializeField] private int totalMoney=0;

    private void OnEnable()
    {
        TriggerEventManager.onMoneyCollected += IncreaseMoney;
    }
    private void OnDisable()
    {
        TriggerEventManager.onMoneyCollected -= IncreaseMoney;
    }
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
    private void IncreaseMoney()
    {
        totalMoney += 5;
    }
}
