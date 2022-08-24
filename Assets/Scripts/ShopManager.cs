using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public List<GameObject> fruitList = new List<GameObject>(); // Olu�turulan meyvelerin tutuldu�u liste
    public List<GameObject> moneyList = new List<GameObject>(); // Olu�turulan meyvelerin tutuldu�u liste
    [Space]
    [Header("Shop Fruit Genarete")]
    [SerializeField] private GameObject fruitPrefab;    // Olu�turulacak obje
    [SerializeField] private GameObject[] fruitPrefabs;    // Olu�turulacak obje
    [SerializeField] private float fruitBetween;
    [SerializeField] private int stackCount = 10;   // Bir s�rada olu�acak meyve say�s�
    [SerializeField] private Transform givePoint;  // Meyvelerin ��kar�laca�� pozisyon
    [Space]
    [Header("Shop Money Genarete")]
    [SerializeField] private GameObject moneyPrefab;    // Olu�turulacak obje
    [SerializeField] private GameObject rotateMoney;    // Olu�turulacak obje
    [SerializeField] private float moneySpawnerTime = 0.5f;  //Meyve olu�turma s�resi
    [SerializeField] private float moneyBetween = 4f;  //Meyveler aras� mesafe
    [SerializeField] private int moneyStackCount = 10;   // Bir s�rada olu�acak meyve say�s�
    [SerializeField] private Transform moneySpawnPoint;  // Meyvelerin ��kar�laca�� pozisyon

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
                //E�er Shop'ta meyve varsa para �ret
                float moneyCount = moneyList.Count;
                int colCount = (int)moneyCount / moneyStackCount;    // Bir s�rada olu�acak para say�s�

                GameObject newMoney = Instantiate(moneyPrefab);   // Yeni para olu�tur
                newMoney.transform.position = new Vector3(moneySpawnPoint.position.x + ((moneyCount % moneyStackCount) / moneyBetween),
                            moneySpawnPoint.position.y+0.05f,
                            moneySpawnPoint.position.z + ((float)colCount / 2));
                moneyList.Add(newMoney); // Yeni olu�turulan paray� moneyList listesine ekle
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
        //GameObject newGiveFruit = Instantiate(TriggerEventManager.farmerManager.fruitPrefab, givePoint);   // Yeni Meyve olu�tur
        //newFruit.transform.position = new Vector3(spawnPoint.position.x+((float)colCount/3),
        //    ((fruitCount%stackCount) / fruitBetween) + 0.1f, 
        //    spawnPoint.position.z);    // Yeni Fruit objesini pozisyonu belirlenir

        // Karakteren gelecek meyveler ile Meyve olu�tur
        float fruitCount = fruitList.Count;
        int colCount = (int)fruitCount / stackCount;    // Bir s�rada olu�acak meyve say�s�

        FruitSelective();  // Karakterin toplad��� meyveler listesinde en �stte bulunan meyveyi ma�azaya ekler      
        GameObject newGiveFruit = Instantiate(fruitPrefab);   // Yeni Meyve olu�tur
        newGiveFruit.transform.position = new Vector3(givePoint.position.x + ((fruitCount % stackCount) / fruitBetween),
                    givePoint.position.y + 0.1f,
                    givePoint.position.z + ((float)colCount / 6));
        fruitList.Add(newGiveFruit); // Yeni olu�turulan meyveyi fruitList listesine ekle
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
    public void RemoveLastMoney()
    {
        // Karaket FarmerField alan�nda meyve toplad���nda son olu�turulan meyveyi toplar ve silinir
        if (moneyList.Count > 0)
        {
            // E�er toplanacak meyve var ise son meyveyi sil
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
