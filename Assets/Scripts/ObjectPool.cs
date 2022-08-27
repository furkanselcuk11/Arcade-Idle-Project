using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Serializable]
    public struct Pool
    {   // Pool Birden fazla farklı nesne ekler
        // Quene= Sıranın sonuna ekleyip, Başından çıkarır
        public Queue<GameObject> pooledObjects; // Oluşturulacak nesneler bir sırada tutulur - Quene,Liste veya dizide tutulablir
        public GameObject objectPrefab;   // Oluşturulacak nesne prefab�
        public int poolSize;  // Oluşturulacak nesne sayısı
    }

    [SerializeField] private Pool[] pools = null;

    private void Awake()
    {   // --- Obje Havuz oluşturma ---        
                
        for (int j = 0; j < pools.Length; j++)
        {
            pools[j].pooledObjects = new Queue<GameObject>(); // Yeni bir sıra oluşturulur
            for (int i = 0; i < pools[j].poolSize; i++)
            {
                // for dögüsü ile"poolSize" Oluşturulacak nesne sayısı kadar yeni nesne oluşturur
                GameObject newObj = Instantiate(pools[j].objectPrefab);  // Yeni oluşturulan nesneleri "newObj" ismi ile oluşturur( Hangi Obje (pool) se�ceçilmiş ise )
                newObj.SetActive(false);    // Başlangıçta tüm nesnenin aktifliği false yapar
                newObj.transform.parent = GameObject.Find("FruitObjects").gameObject.transform; // Oluşturulan objeleri FruitObjects objesinin alt objesi yapar   
                newObj.name = pools[j].objectPrefab.gameObject.name;
                pools[j].pooledObjects.Enqueue(newObj);  // Oluşturulan yeni nesneler sıraya eklenir "poolSize" değeri kadar nesne eklenir
            }
        }
    }
    public GameObject GetPooledObject(int objectType)
    {
        // Aktif edilcek pool objeleri
        if (objectType >= pools.Length) return null;
        GameObject newObj = pools[objectType].pooledObjects.Dequeue();    // Sıranin başından ilk nesneyi çıkarır
        newObj.SetActive(true); // Çıkarılan nesnenin aktifliğini true yapar
        return newObj;
    }
    public void SetPooledObject(GameObject poolObject, int objectType)
    {
        // Pasif edilcek pool objeleri
        if (objectType >= pools.Length) return;
        pools[objectType].pooledObjects.Enqueue(poolObject);  // Çıkarılan nesneyi tekrar sıranınn sonune ekler ve havuzda dongu oluşturur 
        poolObject.SetActive(false);
    }
}