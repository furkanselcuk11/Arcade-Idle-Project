using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    public List<GameObject> fruitList = new List<GameObject>(); // Toplanan meyvelerin tutulduðu liste
    [SerializeField] private GameObject fruitPrefab;    // Oluþturulacak obje
    [SerializeField] private Transform collectPoint;  // Meyvelerin toplanacaðý pozisyon
    [SerializeField] private float fruitBetween = 10f;  //Meyveler arasý mesafe
    [SerializeField] private int fruitCollectLimit = 10; // Karakterin toplayabileceði meyve sayýsý

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
        // Meyve toplama fonksiyonu
        if (fruitList.Count <= fruitCollectLimit)
        {
            // Eðer karakterin topladðý meyve sayýsý fruitCollectLimit sayýsýndan az ise toplar
            GameObject newCollectFruit = Instantiate(TriggerEventManager.farmerManager.fruitPrefab,collectPoint);   // Yeni Meyve oluþtur
            newCollectFruit.transform.position = new Vector3(collectPoint.position.x,
                ((float)fruitList.Count / fruitBetween) + collectPoint.position.y,
                collectPoint.position.z);    // Yeni Fruit objesini pozisyonu belirlenir    
            fruitList.Add(newCollectFruit); // Yeni oluþturulan meyveyi fruitList listesine ekle
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
        // Karaket FarmerField alanýnda meyve topladýðýnda son oluþturulan meyveyi toplar ve silinir
        if (fruitList.Count > 0)
        {
            // Eðer toplanacak meyve var ise son meyveyi sil
            Destroy(fruitList[fruitList.Count - 1]);
            fruitList.RemoveAt(fruitList.Count - 1);
        }
    }
    public void GiveShopFruit()
    {
        // Toplanan meyveleri Maðazaya býrakma fonksiyonu
        if (fruitList.Count > 0)
        {
            // Eðer býrakýlack meyve varsa shopManager objesinden GetFruit fonksiyonu çalýþsýn ve meyve býraksýn
            TriggerEventManager.shopManager.GetFruit();
            RemoveLastFruit();  // Toplanan meyveyi listeden siler
        }
    }
}
