using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventManager : MonoBehaviour
{
    public delegate void OnFruitCollectArea();  // Meyve toplama alan�
    public static event OnFruitCollectArea OnFruitCollet;   // Meyve toplama Eventi
    public delegate void OnFruitGiveArea();  // Meyve b�rakma alan�
    public static event OnFruitGiveArea OnFruitGive;   // Meyve b�rakma Eventi
    public delegate void OnMoneyArea();
    public static event OnMoneyArea onMoneyCollected;

    public static FarmerManager farmerManager;  // FarmerManager eri�im sa�lar
    public static ShopManager shopManager;  // ShopManager eri�im sa�lar
    public static CollectManager collectManager;  //  CollectManager eri�im sa�lar

    [SerializeField] private float fruitCollectTime = 0.5f;  //Meyve toplama s�resi
    bool isCollecting,isGiving,isMoneyColletting;  // Topluyor mu - B�rak�yor mu
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
                OnFruitGive();    // OnFruitGive True oldu�u zamanda OnFruitGive eventi �al���r 
            }
            if (isMoneyColletting)
            {
                onMoneyCollected();
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
            collectManager = other.gameObject.GetComponent<CollectManager>();
        }
        if (other.gameObject.CompareTag("MoneyArea"))
        {
            isMoneyColletting = true;
            shopManager = other.gameObject.GetComponent<ShopManager>();
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
            isMoneyColletting = false;
            shopManager = null;   // E�er Meyve b�rakma alan�ndan ��km��sa eri�imi keser
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Money"))
    //    {
    //        // E�er Karakter Money Triggerine girmi�se
    //        onMoneyCollected();
    //        shopManager.RemoveLastMoney();
    //    }
    //}
}
