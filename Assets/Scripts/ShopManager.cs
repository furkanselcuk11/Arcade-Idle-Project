using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShopManager : MonoBehaviour
{
    public List<GameObject> fruitList = new List<GameObject>(); // Olu�turulan meyvelerin tutuldu�u liste
    public List<GameObject> moneyList = new List<GameObject>(); // Olu�turulan paralar�n tutuldu�u liste
    [Space]
    [Header("Shop Fruit Genarete")]
    [SerializeField] private GameObject fruitPrefab;    // Olu�turulacak meyve
    [SerializeField] private GameObject[] fruitPrefabs;    // Olu�turulacak meyvelerin listesi
    [SerializeField] private float fruitBetween=6;
    [SerializeField] private int stackCount = 10;   // X ekseninde olu�acak  meyve say�s�
    public int maxFruit = 150;   // Max verilecek meyve say�s�
    [SerializeField] private Transform givePoint;  // Meyvelerin ��kar�laca�� pozisyon
    [SerializeField] private GameObject fullFruit;
    bool isWorking;
    [Space]
    [Header("Shop Money Genarete")]
    [SerializeField] private GameObject moneyPrefab;    // Olu�turulacak obje
    [SerializeField] private GameObject rotateMoney;    // Olu�turulacak obje
    [SerializeField] private float moneySpawnerTime = 0.8f;  //Meyve olu�turma s�resi
    [SerializeField] private float moneyBetween = 4f;  //Meyveler aras� mesafe
    [SerializeField] private int moneyStackCountX = 5;   // X ekseninde olu�acak para say�s�
    [SerializeField] private int moneyStackCountY = 25;   // Y ekseninde olu�acak para say�s�
    [SerializeField] private int maxMoney = 100;   // Max verilecek para say�s�
    [SerializeField] private Transform moneySpawnPoint;  // Paralar�n ��kar�laca�� pozisyon
    bool isMoneySpawner;
    [Space]
    [Header("Object Pool")]
    [SerializeField] private ObjectPool objectPool = null;
    [SerializeField] private int poolValue = 0;
    [Space]
    [Header("Shop Money Dotween")]
    [SerializeField] private float duration;    // Shake s�resi
    [SerializeField] private float strength;    // Shake g�c�
    [SerializeField] private int vibrato;   // Titre�im say�s�
    [SerializeField] private float randomness;  // Randomluk

    private void Start()
    {
        StartCoroutine(nameof(GenareteMoney));
    }
    
    IEnumerator GenareteMoney()
    {        
        while (true)
        {
            if (moneyList.Count < maxMoney)
            {
                isMoneySpawner = true;
            }
            else
            {
                isMoneySpawner = false;
            }
            if (fruitList.Count > 0 && isMoneySpawner)
            {
                //E�er Shop'ta meyve varsa para �ret
                float moneyCount = moneyList.Count;
                int colCount = (int)moneyCount / moneyStackCountX;    // Bir s�rada olu�acak para say�s�
                int rowCount = (int)moneyCount / moneyStackCountY;    // Bir s�rada olu�acak para say�s�
                // #objectPool ile ekleme
                    GameObject newMoney = objectPool.GetPooledObject(0);    // "ObjectPool" scriptinden yeni nesne �eker ve aktif hale getirir
                    newMoney.transform.position = new Vector3(moneySpawnPoint.position.x + ((moneyCount % moneyStackCountX) / moneyBetween),
                                moneySpawnPoint.position.y + ((float)rowCount / 10) + 0.05f,
                                moneySpawnPoint.position.z + ((float)colCount / 2) - ((2 * rowCount) + ((float)rowCount / 2f)));
                    newMoney.transform.DOShakeScale(duration, strength, vibrato, randomness);   // Dotween ile Paran�n Scale de�erini b�y�t�p k���lt�r
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
            // E�er Shop para �retmi� ve moneyList en az 1 atne para varsa ise Shop �zerindeki para ikonu aktif hale gelir 
            rotateMoney.SetActive(true);
        }
        else
        {
            rotateMoney.SetActive(false);
        }
        if (fruitList.Count >= maxFruit)
        {
            fullFruit.SetActive(true);
        }
        else
        {
            fullFruit.SetActive(false);
        }
    }
    public void GetFruit()
    {
        // Karakteren gelecek meyveler ile Meyve olu�tur    
        float fruitCount = fruitList.Count;
        int colCount = (int)fruitCount / stackCount;    // Bir s�rada olu�acak meyve say�s�
        if (isWorking)
        {            
            // #objectPool ile ekleme
            FruitSelective();  // Karakterin toplad��� meyveler listesinde en �stte bulunan meyveyinin ismine g�re poolValue de�eri belirlenir   
            GameObject newGiveFruit = objectPool.GetPooledObject(poolValue);    // "ObjectPool" scriptinden yeni nesne �eker ve aktif hale getirir
            newGiveFruit.transform.position = new Vector3(givePoint.position.x + ((fruitCount % stackCount) / fruitBetween),
                        givePoint.position.y + 0.1f,
                        givePoint.position.z + ((float)colCount / 6));
            fruitList.Add(newGiveFruit); // Yeni olu�turulan meyveyi fruitList listesine ekle
            if (fruitList.Count >= maxFruit)
            {
                isWorking = false;  // E�er toplam ��kar�lan Fruit Say�s� maxFruit say�s�na b�y�k e�it ise �al��ma pasif olur                
            }
        }
        else if (fruitList.Count < maxFruit)
        {
            isWorking = true;   // E�er toplam ��kar�lan Fruit Say�s� maxFruit say�s�ndan az ise �al��ma aktif olur                  
        }
    }
    public void RemoveLastFruit()
    {
        // Karaket FarmerField alan�nda meyve toplad���nda son olu�turulan meyveyi toplar ve silinir
        if (fruitList.Count > 0)
        {
            // E�er toplanacak meyve var ise son meyveyi sil
            // #objectPool ile silme
            objectPool.SetPooledObject(fruitList[fruitList.Count - 1], poolValue);  // objectPool ile aktif hale gelen objeyi pasif hale getirir
            // Pasif olan objeyi tekrar FruitObjects �ocu�u yapar d�nd� halinde �al��mas� i�in  
            fruitList.RemoveAt(fruitList.Count - 1);    // fruitList listesinden siler
        }
    }
    public void RemoveLastMoney()
    {
        // Karaket FarmerField alan�nda meyve toplad���nda son olu�turulan meyveyi toplar ve silinir
        if (moneyList.Count > 0)
        {
            // E�er toplanacak meyve var ise son meyveyi sil
            // #objectPool ile silme
            objectPool.SetPooledObject(moneyList[moneyList.Count - 1], 0);  // objectPool ile aktif hale gelen objeyi pasif hale getirir
            // Pasif olan objeyi tekrar FruitObjects �ocu�u yapar d�nd� halinde �al��mas� i�in  
            moneyList.RemoveAt(moneyList.Count - 1);    // fruitList listesinden siler
        }
    }
    void FruitSelective()
    {
        if (TriggerEventManager.collectManager.fruitList[TriggerEventManager.collectManager.fruitList.Count - 1].gameObject.name == "Apple")
        {
            fruitPrefab = fruitPrefabs[0];
            poolValue = 9;
        }
        if (TriggerEventManager.collectManager.fruitList[TriggerEventManager.collectManager.fruitList.Count - 1].gameObject.name == "Banana")
        {
            fruitPrefab = fruitPrefabs[1];
            poolValue = 10;
        }
        if (TriggerEventManager.collectManager.fruitList[TriggerEventManager.collectManager.fruitList.Count - 1].gameObject.name == "Carrot")
        {
            fruitPrefab = fruitPrefabs[2];
            poolValue = 11;
        }
        if (TriggerEventManager.collectManager.fruitList[TriggerEventManager.collectManager.fruitList.Count - 1].gameObject.name == "Mushroom")
        {
            fruitPrefab = fruitPrefabs[3];
            poolValue = 12;
        }
    }
}
