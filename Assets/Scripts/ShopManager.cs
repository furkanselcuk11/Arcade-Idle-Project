using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public List<GameObject> fruitList = new List<GameObject>(); // Oluþturulan meyvelerin tutulduðu liste
    public List<GameObject> moneyList = new List<GameObject>(); // Oluþturulan meyvelerin tutulduðu liste
    [Space]
    [Header("Shop Fruit Genarete")]
    [SerializeField] private GameObject fruitPrefab;    // Oluþturulacak obje
    [SerializeField] private GameObject[] fruitPrefabs;    // Oluþturulacak obje
    [SerializeField] private float fruitBetween;
    [SerializeField] private int stackCount = 10;   // Bir sýrada oluþacak meyve sayýsý
    [SerializeField] private Transform givePoint;  // Meyvelerin Çýkarýlacaðý pozisyon
    [Space]
    [Header("Shop Money Genarete")]
    [SerializeField] private GameObject moneyPrefab;    // Oluþturulacak obje
    [SerializeField] private GameObject rotateMoney;    // Oluþturulacak obje
    [SerializeField] private float moneySpawnerTime = 0.5f;  //Meyve oluþturma süresi
    [SerializeField] private float moneyBetween = 4f;  //Meyveler arasý mesafe
    [SerializeField] private int moneyStackCount = 10;   // Bir sýrada oluþacak meyve sayýsý
    [SerializeField] private Transform moneySpawnPoint;  // Meyvelerin Çýkarýlacaðý pozisyon

    private void Start()
    {
        StartCoroutine(nameof(GenareteMoney));
    }
    
    IEnumerator GenareteMoney()
    {        
        while (true)
        {
            if (fruitList.Count > 0)
            {
                //Eðer Shop'ta meyve varsa para üret
                float moneyCount = moneyList.Count;
                int colCount = (int)moneyCount / moneyStackCount;    // Bir sýrada oluþacak para sayýsý

                GameObject newMoney = Instantiate(moneyPrefab);   // Yeni para oluþtur
                newMoney.transform.position = new Vector3(moneySpawnPoint.position.x + ((moneyCount % moneyStackCount) / moneyBetween),
                            moneySpawnPoint.position.y+0.05f,
                            moneySpawnPoint.position.z + ((float)colCount / 2));
                moneyList.Add(newMoney); // Yeni oluþturulan parayý moneyList listesine ekle
                RemoveLastFruit();
            }
            yield return new WaitForSeconds(moneySpawnerTime);
        }        
    }
    private void Update()
    {
        if (moneyList.Count > 0)
        {
            rotateMoney.SetActive(true);
        }
        else
        {
            rotateMoney.SetActive(false);
        }
    }
    public void GetFruit()
    {
        //GameObject newGiveFruit = Instantiate(TriggerEventManager.farmerManager.fruitPrefab, givePoint);   // Yeni Meyve oluþtur
        //newFruit.transform.position = new Vector3(spawnPoint.position.x+((float)colCount/3),
        //    ((fruitCount%stackCount) / fruitBetween) + 0.1f, 
        //    spawnPoint.position.z);    // Yeni Fruit objesini pozisyonu belirlenir

        // Karakteren gelecek meyveler ile Meyve oluþtur
        float fruitCount = fruitList.Count;
        int colCount = (int)fruitCount / stackCount;    // Bir sýrada oluþacak meyve sayýsý

        FruitSelective();  // Karakterin topladýðý meyveler listesinde en üstte bulunan meyveyi maðazaya ekler      
        GameObject newGiveFruit = Instantiate(fruitPrefab);   // Yeni Meyve oluþtur
        newGiveFruit.transform.position = new Vector3(givePoint.position.x + ((fruitCount % stackCount) / fruitBetween),
                    givePoint.position.y + 0.1f,
                    givePoint.position.z + ((float)colCount / 6));
        fruitList.Add(newGiveFruit); // Yeni oluþturulan meyveyi fruitList listesine ekle
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
    public void RemoveLastMoney()
    {
        // Karaket FarmerField alanýnda meyve topladýðýnda son oluþturulan meyveyi toplar ve silinir
        if (moneyList.Count > 0)
        {
            // Eðer toplanacak meyve var ise son meyveyi sil
            Destroy(moneyList[moneyList.Count - 1]);
            moneyList.RemoveAt(moneyList.Count - 1);
        }
    }
    void FruitSelective()
    {
        if (TriggerEventManager.collectManager.fruitList[TriggerEventManager.collectManager.fruitList.Count - 1].gameObject.name == "Apple")
        {
            fruitPrefab = fruitPrefabs[0];
        }
        if (TriggerEventManager.collectManager.fruitList[TriggerEventManager.collectManager.fruitList.Count - 1].gameObject.name == "Banana")
        {
            fruitPrefab = fruitPrefabs[1];
        }
        if (TriggerEventManager.collectManager.fruitList[TriggerEventManager.collectManager.fruitList.Count - 1].gameObject.name == "Carrot")
        {
            fruitPrefab = fruitPrefabs[2];
        }
        if (TriggerEventManager.collectManager.fruitList[TriggerEventManager.collectManager.fruitList.Count - 1].gameObject.name == "Mushroom")
        {
            fruitPrefab = fruitPrefabs[3];
        }
    }
}
