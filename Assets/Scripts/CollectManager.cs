using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    public List<GameObject> fruitList = new List<GameObject>(); // Toplanan meyvelerin tutuldu�u liste
    [SerializeField] private GameObject fruitPrefab;    // Olu�turulacak obje
    [SerializeField] private Transform collectPoint;  // Meyvelerin toplanaca�� pozisyon
    [SerializeField] private float fruitBetween = 10f;  //Meyveler aras� mesafe
    [SerializeField] private int fruitCollectLimit = 10; // Karakterin toplayabilece�i meyve say�s�

    private void OnEnable()
    {
        TriggerEventManager.OnFruitCollet += GetFruit; //E�er Meyve toplama alan�n da ise GetFruit fonkisyonu �al���r
        TriggerEventManager.OnFruitGive += GiveShopFruit; //E�er Meyve b�rakma alan�n da ise GiveShopFruit fonkisyonu �al���r
    }
    private void OnDisable()
    {
        TriggerEventManager.OnFruitCollet -= GetFruit;  // E�er Meyve toplama alan�ndan ��km��sa GetFruit fonkisyonu durdurulur
        TriggerEventManager.OnFruitGive -= GiveShopFruit;  // E�er Meyve b�rakma alan�ndan ��km��sa GiveShopFruit fonkisyonu durdurulur

    }
    private void GetFruit()
    {
        // Meyve toplama fonksiyonu
        if (fruitList.Count <= fruitCollectLimit)
        {
            // E�er karakterin toplad�� meyve say�s� fruitCollectLimit say�s�ndan az ise toplar
            GameObject newCollectFruit = Instantiate(TriggerEventManager.farmerManager.fruitPrefab,collectPoint);   // Yeni Meyve olu�tur
            newCollectFruit.name = TriggerEventManager.farmerManager.fruitPrefab.name;
            newCollectFruit.transform.position = new Vector3(collectPoint.position.x,
                ((float)fruitList.Count / fruitBetween) + collectPoint.position.y,
                collectPoint.position.z);    // Yeni Fruit objesini pozisyonu belirlenir    
            fruitList.Add(newCollectFruit); // Yeni olu�turulan meyveyi fruitList listesine ekle
            if (TriggerEventManager.farmerManager != null)
            {
                // Temas ett�i objenin i�inde farmerManager varsa 
                // farmerManager objesinin RemoveLastFruit fonksiyonu ile son meyveleri siler
                TriggerEventManager.farmerManager.RemoveLastFruit();
            }
        }
    }
    public void RemoveLastFruit()
    {
        // Karaket FarmerField alan�nda meyve toplad���nda son olu�turulan meyveyi toplar ve silinir
        if (fruitList.Count > 0)
        {
            // E�er toplanacak meyve var ise son meyveyi sil
            Destroy(fruitList[fruitList.Count - 1]);
            fruitList.RemoveAt(fruitList.Count - 1);
        }
    }
    public void GiveShopFruit()
    {
        // Toplanan meyveleri Ma�azaya b�rakma fonksiyonu
        if (fruitList.Count > 0)
        {
            // E�er b�rak�lack meyve varsa shopManager objesinden GetFruit fonksiyonu �al��s�n ve meyve b�raks�n
            TriggerEventManager.shopManager.GetFruit();
            RemoveLastFruit();  // Toplanan meyveyi listeden siler
        }
    }
}
