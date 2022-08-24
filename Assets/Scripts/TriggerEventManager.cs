using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventManager : MonoBehaviour
{
    public delegate void OnFruitCollectArea();  // Meyve toplama alaný
    public static event OnFruitCollectArea OnFruitCollet;   // Meyve toplama Eventi
    public delegate void OnFruitGiveArea();  // Meyve býrakma alaný
    public static event OnFruitGiveArea OnFruitGive;   // Meyve býrakma Eventi
    public delegate void OnMoneyArea(); // Para toplama alaný
    public static event OnMoneyArea OnMoneyCollected;   // Para toplama Eventi
    public delegate void OnBuyArea();   // Satýn alma alaný
    public static event OnBuyArea OnBuyShopAndFarmer; // Satýn alma alaný Eventi

    public static FarmerManager farmerManager;  // FarmerManager eriþim saðlar
    public static ShopManager shopManager;  // ShopManager eriþim saðlar
    public static CollectManager collectManager;  //  CollectManager eriþim saðlar
    public static BuyArea buyArea;

    [SerializeField] private float fruitCollectTime = 0.5f;  //Meyve toplama süresi
    bool isCollecting,isGiving,isTakeMoney;  // Topluyor mu - Býrakýyor mu
    void Start()
    {
        StartCoroutine(nameof(CollectEnum)); //CollectEnum fonksiyonu baþlangýçta çalýþtýr
    }  
    IEnumerator CollectEnum()
    {
        // Toplama ve Verme iþlemleri
        while (true)
        {
            if (isCollecting)
            {
                // OnFruitCollet eventi ile birlikte istenilen yerden fonksiyon çalýþtýrýllýr
                OnFruitCollet();    // isCollecting True olduðu zamanda OnFruitCollet eventi çalýþýr                
            }
            if (isGiving)
            {
                // OnFruitGive eventi ile birlikte istenilen yerden fonksiyon çalýþtýrýllýr
                OnFruitGive();    // isGiving True olduðu zamanda OnFruitGive eventi çalýþýr 
            }
            if (isTakeMoney)
            {
                // OnMoneyCollected eventi ile birlikte istenilen yerden fonksiyon çalýþtýrýllýr
                OnMoneyCollected();    // isTakeMoney True olduðu zamanda OnMoneyCollected eventi çalýþýr 
            }
            yield return new WaitForSeconds(fruitCollectTime);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("FruitCollectArea"))
        {
            // Eðer Meyve toplama alanýn da ise isCollecting True olur
            isCollecting = true;
            // Eðer Meyve toplama alanýn da ise FarmerManager objesine eriþir
            farmerManager = other.gameObject.GetComponent<FarmerManager>();            
        }
        if (other.gameObject.CompareTag("FruitGiveArea"))
        {
            // Eðer Meyve býrakma alanýn da ise isGiving True olur
            isGiving = true;
            // Eðer Meyve býrakma alanýn da ise ShopManager objesine eriþir
            shopManager = other.gameObject.GetComponent<ShopManager>();
            // Eðer Meyve býrakma alanýn da ise CollectManager objesine eriþir
            collectManager = gameObject.GetComponent<CollectManager>();
        }
        if (other.gameObject.CompareTag("MoneyArea"))
        {
            // Eðer Para toplama alanýn da ise isTakeMoney True olur
            isTakeMoney = true;
            // Eðer Para býrakma alanýn da ise ShopManager objesine eriþir
            shopManager = other.transform.parent.gameObject.GetComponent<ShopManager>();
        }
        if (other.gameObject.CompareTag("BuyArea"))
        {
            OnBuyShopAndFarmer();
            // Eðer BuyArea alanýn da ise BuyArea objesine eriþir
            buyArea = other.gameObject.GetComponent<BuyArea>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("FruitCollectArea"))
        {
            
            isCollecting = false;// Eðer Meyve toplama alanýndan çýkmýþsa isCollecting pasif olur
            farmerManager = null;   // Eðer Meyve toplama alanýndan çýkmýþsa eriþimi keser
        }
        if (other.gameObject.CompareTag("FruitGiveArea"))
        {            
            isGiving = false;// Eðer Meyve býrakma alanýndan çýkmýþsa isGiving pasif olur
            shopManager = null;   // Eðer Meyve býrakma alanýndan çýkmýþsa eriþimi keser
            collectManager = null;   // Eðer Meyve býrakma alanýndan çýkmýþsa eriþimi keser
        }
        if (other.gameObject.CompareTag("MoneyArea"))
        {
            
            isTakeMoney = false;   // Eðer Para toplama alanýn da ise isTakeMoney pasif olur
            shopManager = null; // Eðer Para býrakma alanýndan çýkmýþsa eriþimi keser
        }
        if (other.gameObject.CompareTag("BuyArea"))
        {            
            buyArea = null; // Eðer BuyArea alanýndan çýkmýþsa eriþimi keser
        }
    }
}
