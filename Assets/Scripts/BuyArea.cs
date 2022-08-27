using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyArea : MonoBehaviour
{
    [Space]
    [Header("Shop Area Take")]
    [SerializeField] private GameObject farmerAndShopObject, buyObject;
    [SerializeField] private int cost;    // Sat�n al�nacak ��enin maliyeti
    [SerializeField] private int currentMoney; // �denen para miktar�
    [SerializeField] private float progress;
    [SerializeField] private Image progressImage;
    [SerializeField] private TextMeshProUGUI areaText;

    [Space]
    [Header("Shop Money Dotween")]
    [SerializeField] private float duration;    // Shake s�resi
    [SerializeField] private float strength;    // Shake g�c�
    [SerializeField] private int vibrato;   // Titre�im say�s�
    [SerializeField] private float randomness;  // Randomluk

    private void Start()
    {
        areaText.text = " $ " + (cost-currentMoney);
    }
    public void Buy(int valueMoney)
    {
        if (currentMoney <cost)
        {
            currentMoney += valueMoney;
            areaText.text = " $ " + (cost - currentMoney);
            progress = (currentMoney / cost);
            progressImage.fillAmount = progress;
            if (currentMoney == cost)
            {
                // E�er sat�n alma tamamland�ysa
                buyObject.SetActive(false); // Sat�n alma triggerini pasif yapar
                farmerAndShopObject.SetActive(true);    // Sat�n al�nan ��eyi aktif hale getir
                farmerAndShopObject.transform.DOShakeScale(duration, strength, vibrato, randomness);   // Dotween ile Paran�n Scale de�erini b�y�t�p k���lt�r
                this.enabled = false;   // Sat�n alma i�lemi tamamlan�nca kodu kapat
            }
        }

    }
}
