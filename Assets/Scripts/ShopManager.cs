using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShopManager : MonoBehaviour
{
    public List<GameObject> fruitList = new List<GameObject>(); // Oluþturulan meyvelerin tutulduðu liste
    public List<GameObject> moneyList = new List<GameObject>(); // Oluþturulan meyvelerin tutulduðu liste
    [Space]
    [Header("Shop Fruit Genarete")]
    [SerializeField] private GameObject fruitPrefab;    // Oluþturulacak meyve
    [SerializeField] private GameObject[] fruitPrefabs;    // Oluþturulacak meyvelerin listesi
    [SerializeField] private float fruitBetween=6;
    [SerializeField] private int stackCount = 10;   // X ekseninde oluþacak  meyve sayýsý
    [SerializeField] private Transform givePoint;  // Meyvelerin Çýkarýlacaðý pozisyon
    [Space]
    [Header("Shop Money Genarete")]
    [SerializeField] private GameObject moneyPrefab;    // Oluþturulacak obje
    [SerializeField] private GameObject rotateMoney;    // Oluþturulacak obje
    [SerializeField] private float moneySpawnerTime = 0.8f;  //Meyve oluþturma süresi
    [SerializeField] private float moneyBetween = 4f;  //Meyveler arasý mesafe
    [SerializeField] private int moneyStackCountX = 5;   // X ekseninde oluþacak para sayýsý
    [SerializeField] private int moneyStackCountY = 25;   // Y ekseninde oluþacak para sayýsý
    [SerializeField] private Transform moneySpawnPoint;  // Paralarýn Çýkarýlacaðý pozisyon
    [Space]
    [Header("Shop Money Dotween")]
    [SerializeField] private float duration;    // Shake süresi
    [SerializeField] private float strength;    // Shake gücü
    [SerializeField] private int vibrato;   // Titreþim sayýsý
    [SerializeField] private float randomness;  // Randomluk

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
                int colCount = (int)moneyCount / moneyStackCountX;    // Bir sýrada oluþacak para sayýsý
                int rowCount = (int)moneyCount / moneyStackCountY;    // Bir sýrada oluþacak para sayýsý

                GameObject newMoney = Instantiate(moneyPrefab);   // Yeni para oluþtur
                newMoney.transform.position = new Vector3(moneySpawnPoint.position.x + ((moneyCount % moneyStackCountX) / moneyBetween),
                            moneySpawnPoint.position.y + ((float)rowCount / 10)+0.05f,
                            moneySpawnPoint.position.z+ ((float)colCount / 2) - ((2 * rowCount) + ((float)rowCount / 2f)));
                newMoney.transform.DOShakeScale(duration, strength, vibrato, randomness);
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
            // Eðer Shop para üretmiþ ve moneyList en az 1 atne para varsa ise Shop üzerindeki para ikonu aktif hale gelir 
            rotateMoney.SetActive(true);
        }
        else
        {
            rotateMoney.SetActive(false);
        }
    }
    public void GetFruit()
    {        

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
