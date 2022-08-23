using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerManager : MonoBehaviour
{
    public List<GameObject> fruitList = new List<GameObject>(); // Olu�turulan meyvelerin tutuldu�u liste    

    public GameObject fruitPrefab;    // Olu�turulacak obje
    [SerializeField] private float spawnerTime = 0.5f;  //Meyve olu�turma s�resi
    [SerializeField] private float fruitBetween = 10f;  //Meyveler aras� mesafe
    [SerializeField] private int maxFruit = 50; // Toplam ��karilacak meyve say�s�
    [SerializeField] private int stackCount = 10;   // Bir s�rada olu�acak meyve say�s�
    [SerializeField] private Transform spawnPoint;  // Meyvelerin ��kar�laca�� pozisyon
    bool isWorking;
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
            int colCount = (int)fruitCount / stackCount;    // Bir s�rada olu�acak meyve say�s�

            if (isWorking)  // E�er farmer �al��yorsa
            {
                GameObject newFruit = Instantiate(fruitPrefab);   // Yeni Meyve olu�tur
                newFruit.transform.position = new Vector3(spawnPoint.position.x+((fruitCount % stackCount) / fruitBetween),
                    spawnPoint.position.y+0.1f,
                    spawnPoint.position.z + ((float)colCount / 3));    
                fruitList.Add(newFruit); // Yeni olu�turulan meyveyi fruitList listesine ekle

                if (fruitList.Count>=maxFruit)
                {
                    isWorking = false;  // E�er toplam ��kar�lan Fruit Say�s� maxFruit say�s�na b�y�k e�it ise �al��ma pasif olur
                }
            }
            else if (fruitList.Count < maxFruit)
            {
                isWorking = true;   // E�er toplam ��kar�lan Fruit Say�s� maxFruit say�s�ndan az ise �al��ma aktif olur
            }            

            yield return new WaitForSeconds(spawnerTime);   // Her spawnerTime s�resinde bir Meyve olu�tur
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
}