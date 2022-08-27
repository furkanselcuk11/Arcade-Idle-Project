using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TreeController : MonoBehaviour
{
    [Space]
    [Header("Farmer Fruit Dotween")]
    private GameObject farmer;
    [SerializeField] private float duration;    // Shake s�resi
    [SerializeField] private float strength;    // Shake g�c�
    [SerializeField] private int vibrato;   // Titre�im say�s�
    [SerializeField] private float randomness;  // Randomluk
    [SerializeField] private float shakeTime = 0.5f;
    private Animator anim;
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
