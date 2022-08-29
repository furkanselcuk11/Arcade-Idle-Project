using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TreeController : MonoBehaviour
{
    [Space]
    [Header("Farmer Fruit Dotween")]
    private GameObject farmer;
    [SerializeField] private float duration;    // Shake süresi
    [SerializeField] private float strength;    // Shake gücü
    [SerializeField] private int vibrato;   // Titreþim sayýsý
    [SerializeField] private float randomness;  // Randomluk
    [SerializeField] private float shakeTime = 0.5f;
    private Animator anim;
    private bool isWork;
    void Start()
    {
        this.farmer = this.transform.parent.gameObject.transform.GetComponent<FarmerManager>().gameObject;
        this.anim = this.farmer.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        StartCoroutine(nameof(ShakeTree));
    }
    void Update()
    {
        if (farmer.gameObject.GetComponent<FarmerManager>().isWorking)
        {
            // Eðer Farmer alanýnda meyve üretimi var ise Farmer karakterinin isWorking animsyonu çalýþýr
            this.anim.SetBool("isWorking", true);
        }
        else
        {
            this.anim.SetBool("isWorking", false);
        }
    }
    IEnumerator ShakeTree()
    {
        while (true)
        {
            if (farmer.gameObject.GetComponent<FarmerManager>().isWorking)
            {
                this.transform.DOShakeRotation(duration, strength, vibrato, randomness);
            }
            yield return new WaitForSeconds(shakeTime);
        }
    }
}
