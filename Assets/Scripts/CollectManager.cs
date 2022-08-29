using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    public List<GameObject> fruitList = new List<GameObject>(); // Toplanan meyvelerin tutulduðu liste
    [SerializeField] private Transform collectPoint;  // Meyvelerin toplanacaðý pozisyon
    [SerializeField] private float fruitBetween = 10f;  //Meyveler arasý mesafe
    [SerializeField] private int fruitCollectLimit = 10; // Karakterin toplayabileceði meyve sayýsý
    [Space]
    [Header("Object Pool")]
    [SerializeField] private ObjectPool objectPool = null;
    [SerializeField] private int poolValue = 0;

    private void OnEnable()
    {
        TriggerEventManager.OnFruitCollet += GetFruit; //Eðer Meyve toplama alanýn da ise GetFruit fonkisyonu çalýþýr
        TriggerEventManager.OnFruitGive += GiveShopFruit; //Eðer Meyve býrakma alanýn da ise GiveShopFruit fonkisyonu çalýþýr
    }
    private void OnDisable()
    {
        TriggerEventManager.OnFruitCollet -= GetFruit;  // Eðer Meyve toplama alanýndan çýkmýþsa GetFruit fonkisyonu durdurulur
        TriggerEventManager.OnFruitGive -= GiveShopFruit;  // Eðer Meyve býrakma alanýndan çýkmýþsa GiveShopFruit fonkisyonu durdurulur

    }
    private void GetFruit()
    {
        // Meyve toplama fonksiyonu (Farmerden)
        // #objectPool ile ekleme
        if (fruitList.Count < fruitCollectLimit)
        {
            poolValue = TriggerEventManager.farmerManager.publicpoolValue+4;  // poolValue deðeri FarmerField alanýnda üretilen poolObjectin dizi numarýsýný alýr ve o objeyi seçer
            // Eðer karakterin topladðý meyve sayýsý fruitCollectLimit sayýsýndan az ise toplar
            GameObject newCollectFruit = objectPool.GetPooledObject(poolValue);    // "ObjectPool" scriptinden yeni nesne çeker ve aktif hale getirirr
            newCollectFruit.transform.parent = collectPoint;    // Aktif olan objeyi collectPoint çocuðu yapar 
            newCollectFruit.transform.position = new Vector3(collectPoint.position.x,
                ((float)fruitList.Count / fruitBetween) + collectPoint.position.y,
                collectPoint.position.z);    // Yeni Fruit objesini pozisyonu belirlenir    
            fruitList.Add(newCollectFruit); // Yeni oluþturulan meyveyi fruitList listesine ekle
            AudioController.audioControllerInstance.Play("FruitSound"); // Her meyve toplandýðýnda ses çalýþýr
            if (TriggerEventManager.farmerManager != null)
            {
                // Temas ettði objenin içinde farmerManager varsa 
                // farmerManager objesinin RemoveLastFruit fonksiyonu ile son meyveleri siler
                TriggerEventManager.farmerManager.RemoveLastFruit();
            }
        }
    }
    public void RemoveLastFruit()
    {
        // Karaker FarmerField alanýnda meyve topladýðýnda son oluþturulan meyveyi toplar ve silinir
        if (fruitList.Count > 0)
        {
            // Eðer toplanacak meyve var ise son meyveyi sil          
            // #objectPool ile silme
            objectPool.SetPooledObject(fruitList[fruitList.Count - 1], poolValue);  // objectPool ile aktif hale gelen objeyi pasif hale getirir
            fruitList[fruitList.Count - 1].transform.parent = GameObject.Find("FruitObjects").gameObject.transform; 
            // Pasif olan objeyi tekrar FruitObjects çocuðu yapar - objecool döngü halinde çalýþmasý için  
            fruitList.RemoveAt(fruitList.Count - 1);    // fruitList listesinden siler
        }
    }
    public void GiveShopFruit()
    {
        // Toplanan meyveleri Maðazaya býrakma fonksiyonu
        if (fruitList.Count > 0)
        {
            // Eðer býrakýlack meyve varsa shopManager objesinden GetFruit fonksiyonu çalýþsýn ve meyve býraksýn
            if (TriggerEventManager.shopManager.fruitList.Count < TriggerEventManager.shopManager.maxFruit)
            {
                TriggerEventManager.shopManager.GetFruit();
                RemoveLastFruit();  // Toplanan meyveyi listeden siler
            }
            
        }
    }
}
