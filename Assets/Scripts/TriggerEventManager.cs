using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventManager : MonoBehaviour
{
    public delegate void OnFruitCollectArea();  // Meyve toplama alan�
    public static event OnFruitCollectArea OnFruitCollet;   // Meyve toplama Eventi
    public delegate void OnFruitGiveArea();  // Meyve b�rakma alan�
    public static event OnFruitGiveArea OnFruitGive;   // Meyve b�rakma Eventi
    public delegate void OnMoneyArea(); // Para toplama alan�
    public static event OnMoneyArea OnMoneyCollected;   // Para toplama Eventi
    public delegate void OnBuyArea();   // Sat�n alma alan�
    public static event OnBuyArea OnBuyShopAndFarmer; // Sat�n alma alan� Eventi

    public static FarmerManager farmerManager;  // FarmerManager eri�im sa�lar
    public static ShopManager shopManager;  // ShopManager eri�im sa�lar
    public static CollectManager collectManager;  //  CollectManager eri�im sa�lar
    public static BuyArea buyArea;

    [SerializeField] private float fruitCollectTime = 0.5f;  //Meyve toplama s�resi
    bool isCollecting,isGiving,isTakeMoney;  // Topluyor mu - B�rak�yor mu
    void Start()
    {
        StartCoroutine(nameof(CollectEnum)); //CollectEnum fonksiyonu ba�lang��ta �al��t�r
    }  
    IEnumerator CollectEnum()
    {
        // Toplama ve Verme i�lemleri
        while (true)
        {
            if (isCollecting)
            {
                // OnFruitCollet eventi ile birlikte istenilen yerden fonksiyon �al��t�r�ll�r
                OnFruitCollet();    // isCollecting True oldu�u zamanda OnFruitCollet eventi �al���r                
            }
            if (isGiving)
            {
                // OnFruitGive eventi ile birlikte istenilen yerden fonksiyon �al��t�r�ll�r
                OnFruitGive();    // isGiving True oldu�u zamanda OnFruitGive eventi �al���r 
            }
            if (isTakeMoney)
            {
                // OnMoneyCollected eventi ile birlikte istenilen yerden fonksiyon �al��t�r�ll�r
                OnMoneyCollected();    // isTakeMoney True oldu�u zamanda OnMoneyCollected eventi �al���r 
            }
            yield return new WaitForSeconds(fruitCollectTime);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("FruitCollectArea"))
        {
            // E�er Meyve toplama alan�n da ise isCollecting True olur
            isCollecting = true;
            // E�er Meyve toplama alan�n da ise FarmerManager objesine eri�ir
            farmerManager = other.gameObject.GetComponent<FarmerManager>();            
        }
        if (other.gameObject.CompareTag("FruitGiveArea"))
        {
            // E�er Meyve b�rakma alan�n da ise isGiving True olur
            isGiving = true;
            // E�er Meyve b�rakma alan�n da ise ShopManager objesine eri�ir
            shopManager = other.gameObject.GetComponent<ShopManager>();
            // E�er Meyve b�rakma alan�n da ise CollectManager objesine eri�ir
            collectManager = gameObject.GetComponent<CollectManager>();
        }
        if (other.gameObject.CompareTag("MoneyArea"))
        {
            // E�er Para toplama alan�n da ise isTakeMoney True olur
            isTakeMoney = true;
            // E�er Para b�rakma alan�n da ise ShopManager objesine eri�ir
            shopManager = other.transform.parent.gameObject.GetComponent<ShopManager>();
        }
        if (other.gameObject.CompareTag("BuyArea"))
        {
            OnBuyShopAndFarmer();
            // E�er BuyArea alan�n da ise BuyArea objesine eri�ir
            buyArea = other.gameObject.GetComponent<BuyArea>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("FruitCollectArea"))
        {
            
            isCollecting = false;// E�er Meyve toplama alan�ndan ��km��sa isCollecting pasif olur
            farmerManager = null;   // E�er Meyve toplama alan�ndan ��km��sa eri�imi keser
        }
        if (other.gameObject.CompareTag("FruitGiveArea"))
        {            
            isGiving = false;// E�er Meyve b�rakma alan�ndan ��km��sa isGiving pasif olur
            shopManager = null;   // E�er Meyve b�rakma alan�ndan ��km��sa eri�imi keser
            collectManager = null;   // E�er Meyve b�rakma alan�ndan ��km��sa eri�imi keser
        }
        if (other.gameObject.CompareTag("MoneyArea"))
        {
            
            isTakeMoney = false;   // E�er Para toplama alan�n da ise isTakeMoney pasif olur
            shopManager = null; // E�er Para b�rakma alan�ndan ��km��sa eri�imi keser
        }
        if (other.gameObject.CompareTag("BuyArea"))
        {            
            buyArea = null; // E�er BuyArea alan�ndan ��km��sa eri�imi keser
        }
    }
}
