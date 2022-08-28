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
    [SerializeField] private int cost;    // Satýn alýnacak öðenin maliyeti
    [SerializeField] private int currentMoney; // Ödenen para miktarý
    [SerializeField] private float progress;
    [SerializeField] private Image progressImage;
    [SerializeField] private TextMeshProUGUI areaText;

    [Space]
    [Header("Shop Money Dotween")]
    [SerializeField] private float duration;    // Shake süresi
    [SerializeField] private float strength;    // Shake gücü
    [SerializeField] private int vibrato;   // Titreþim sayýsý
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
                // Eðer satýn alma tamamlandýysa
                buyObject.SetActive(false); // Satýn alma triggerini pasif yapar
                farmerAndShopObject.SetActive(true);    // Satýn alýnan öðeyi aktif hale getir
                farmerAndShopObject.transform.DOShakeScale(duration, strength, vibrato, randomness);   // Dotween ile Paranýn Scale deðerini büyütüp küçültür
                AudioController.audioControllerInstance.Play("BuyAreaSound"); // Yeni alan satýn alýndýðýnda ses çalýþýr
                this.enabled = false;   // Satýn alma iþlemi tamamlanýnca kodu kapat
            }
        }

    }
}
