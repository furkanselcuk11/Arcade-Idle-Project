using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FarmerManager : MonoBehaviour
{
    public List<GameObject> fruitList = new List<GameObject>(); // Oluþturulan meyvelerin tutulduðu liste    
    [Space]
    [Header("Fruit Spawner")]
    public GameObject fruitPrefab;    // Oluþturulacak obje
    [SerializeField] private float spawnerTime = 0.5f;  //Meyve oluþturma süresi
    [SerializeField] private float fruitBetween = 10f;  //Meyveler arasý mesafe
    [SerializeField] private int maxFruit = 50; // Toplam çýkarilacak meyve sayýsý
    [SerializeField] private int stackCount = 10;   // Bir sýrada oluþacak meyve sayýsý
    [SerializeField] private Transform spawnPoint;  // Meyvelerin Çýkarýlacaðý pozisyon
    [SerializeField] private GameObject fullFruit;
    public bool isWorking;
    [Space]
    [Header("Object Pool")]
    [SerializeField] private ObjectPool objectPool = null;
    [SerializeField] private int poolValue = 0;
    public int publicpoolValue
    {
        get { return poolValue; }
        set { poolValue = value; }
    }

    [Space]
    [Header("Fruit Jump Dotween")]
    [SerializeField] private GameObject jumpFruitObject;
    [SerializeField] private Vector3 jumpFruitObjectStartPositoin;
    [SerializeField] private float jumpPower;    // Zýplama gücü
    [SerializeField] private int jumpCount;    // Zýplama sayýsý
    [SerializeField] private float duration;   // Zýplama süresi
    void Start()
    {
        StartCoroutine(nameof(FarmerFruitSpawner));
    }    
    void Update()
    {
        
    }
    IEnumerator FarmerFruitSpawner()
    {
        while (true)
        {
            float fruitCount = fruitList.Count;
            int colCount = (int)fruitCount / stackCount;    // Bir sýrada oluþacak meyve sayýsý    

            // #objectPool ile ekleme
            if (isWorking)  // Eðer farmer çalþýyorsa
            {
                GameObject newFruit = objectPool.GetPooledObject(poolValue);    // "ObjectPool" scriptinden yeni nesne çeker ve aktif hale getirir
                newFruit.transform.position = new Vector3(spawnPoint.position.x + ((fruitCount % stackCount) / fruitBetween),
                    spawnPoint.position.y + 0.1f,
                    spawnPoint.position.z + ((float)colCount / 3));
                fruitList.Add(newFruit); // Yeni oluþturulan meyveyi fruitList listesine ekle                
                jumpFruitObject.transform.DOMove(new Vector3(newFruit.transform.position.x, 0f, newFruit.transform.position.z), duration);
                jumpFruitObject.transform.localPosition = jumpFruitObjectStartPositoin;

                if (fruitList.Count >= maxFruit)
                {
                    isWorking = false;  // Eðer toplam çýkarýlan Fruit Sayýsý maxFruit sayýsýna büyük eþit ise çalýþma pasif olur
                    fullFruit.SetActive(true);
                }
            }
            else if (fruitList.Count < maxFruit)
            {
                isWorking = true;   // Eðer toplam çýkarýlan Fruit Sayýsý maxFruit sayýsýndan az ise çalýþma aktif olur
                fullFruit.SetActive(false);
            }

            yield return new WaitForSeconds(spawnerTime);   // Her spawnerTime süresinde bir Meyve oluþtur
        }        
    }
    public void RemoveLastFruit()
    {
        // Karaket FarmerField alanýnda meyve topladýðýnda son oluþturulan meyveyi toplar ve silinir
        if (fruitList.Count > 0)
        {
            // Eðer toplanacak meyve var ise son meyveyi sil
            // #objectPool ile silme
            objectPool.SetPooledObject(fruitList[fruitList.Count - 1], poolValue);  // objectPool ile aktif hale gelen objeyi pasif hale getirir
            fruitList.RemoveAt(fruitList.Count - 1);    // fruitList listesinden siler
        }
    }
}
