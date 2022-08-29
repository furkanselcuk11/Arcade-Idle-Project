using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    public List<GameObject> fruitList = new List<GameObject>(); // Toplanan meyvelerin tutuldu�u liste
    [SerializeField] private Transform collectPoint;  // Meyvelerin toplanaca�� pozisyon
    [SerializeField] private float fruitBetween = 10f;  //Meyveler aras� mesafe
    [SerializeField] private int fruitCollectLimit = 10; // Karakterin toplayabilece�i meyve say�s�
    [Space]
    [Header("Object Pool")]
    [SerializeField] private ObjectPool objectPool = null;
    [SerializeField] private int poolValue = 0;

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
        // Meyve toplama fonksiyonu (Farmerden)
        // #objectPool ile ekleme
        if (fruitList.Count < fruitCollectLimit)
        {
            poolValue = TriggerEventManager.farmerManager.publicpoolValue+4;  // poolValue de�eri FarmerField alan�nda �retilen poolObjectin dizi numar�s�n� al�r ve o objeyi se�er
            // E�er karakterin toplad�� meyve say�s� fruitCollectLimit say�s�ndan az ise toplar
            GameObject newCollectFruit = objectPool.GetPooledObject(poolValue);    // "ObjectPool" scriptinden yeni nesne �eker ve aktif hale getirirr
            newCollectFruit.transform.parent = collectPoint;    // Aktif olan objeyi collectPoint �ocu�u yapar 
            newCollectFruit.transform.position = new Vector3(collectPoint.position.x,
                ((float)fruitList.Count / fruitBetween) + collectPoint.position.y,
                collectPoint.position.z);    // Yeni Fruit objesini pozisyonu belirlenir    
            fruitList.Add(newCollectFruit); // Yeni olu�turulan meyveyi fruitList listesine ekle
            AudioController.audioControllerInstance.Play("FruitSound"); // Her meyve topland���nda ses �al���r
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
        // Karaker FarmerField alan�nda meyve toplad���nda son olu�turulan meyveyi toplar ve silinir
        if (fruitList.Count > 0)
        {
            // E�er toplanacak meyve var ise son meyveyi sil          
            // #objectPool ile silme
            objectPool.SetPooledObject(fruitList[fruitList.Count - 1], poolValue);  // objectPool ile aktif hale gelen objeyi pasif hale getirir
            fruitList[fruitList.Count - 1].transform.parent = GameObject.Find("FruitObjects").gameObject.transform; 
            // Pasif olan objeyi tekrar FruitObjects �ocu�u yapar - objecool d�ng� halinde �al��mas� i�in  
            fruitList.RemoveAt(fruitList.Count - 1);    // fruitList listesinden siler
        }
    }
    public void GiveShopFruit()
    {
        // Toplanan meyveleri Ma�azaya b�rakma fonksiyonu
        if (fruitList.Count > 0)
        {
            // E�er b�rak�lack meyve varsa shopManager objesinden GetFruit fonksiyonu �al��s�n ve meyve b�raks�n
            if (TriggerEventManager.shopManager.fruitList.Count < TriggerEventManager.shopManager.maxFruit)
            {
                TriggerEventManager.shopManager.GetFruit();
                RemoveLastFruit();  // Toplanan meyveyi listeden siler
            }
            
        }
    }
}
