using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyArea : MonoBehaviour
{
    [SerializeField] private BuyAreaSO buyAreaType = null;    // Scriptable Objects eriþir  
    [Space]
    [Header("Shop Area Take")]
    [SerializeField] private GameObject farmerAndShopObject, buyObject;
    //[SerializeField] private int cost;    // Satın alınacak öğenin maliyeti
    //[SerializeField] private int currentMoney; // Ödenen para miktarı
    [SerializeField] private float progress;
    [SerializeField] private Image progressImage;
    [SerializeField] private TextMeshProUGUI areaText;
    public bool areaLocked;

    [Space]
    [Header("Shop Money Dotween")]
    [SerializeField] private float duration;    // Shake süresi
    [SerializeField] private float strength;    // Shake gücü
    [SerializeField] private int vibrato;   // Titreşim sayısı
    [SerializeField] private float randomness;  // Randomluk

    

    private void Start()
    {
        areaText.text = "$ " + (buyAreaType.cost - buyAreaType.currentMoney);
        this.areaLocked = buyAreaType.locked;
        if (!buyAreaType.locked)
        {
            // Bina açık mı değil mi *- Açık ise kodu kapat ve bina açık kalsın
            this.buyObject.SetActive(false); // Satın alma triggerini pasif yapar
            this.farmerAndShopObject.SetActive(true);    // Satın alınan öğeyi aktif hale getir
            this.enabled = false;   // Satın alma işlemi tamamlanınca kodu kapat            
        }
    }
    public void Buy(int valueMoney)
    {
        if (buyAreaType.currentMoney < buyAreaType.cost)
        {
            buyAreaType.currentMoney += valueMoney; // Satın alma için ödenen para miktarını arttır
            areaText.text = "$ " + (buyAreaType.cost - buyAreaType.currentMoney);   // Kalan para mitarnı göster
            progress = (buyAreaType.currentMoney / buyAreaType.cost);   
            progressImage.fillAmount = progress;    // Ödenen miktarın bar gösterimi
            SaveManager.savemanagerInstance.SaveGame(); // Verileri kaydet

            if (buyAreaType.currentMoney == buyAreaType.cost)
            {
                // Eğer satın alma işlemi tamamlandıysa
                this.buyAreaType.locked = false;    // Bina satın alınma kilidi açar
                this.areaLocked = buyAreaType.locked;   // Bina satın alınma kilidi incpector'da gösterir
                this.buyObject.SetActive(false);    // Satın alma alanını pasif yapar
                this.farmerAndShopObject.SetActive(true);    // Satın alınan öğeyi aktif hale getir
                this.farmerAndShopObject.transform.DOShakeScale(duration, strength, vibrato, randomness);   // Dotween ile Açılan binanın Scale değerini büyütüp küçültür
                AudioController.audioControllerInstance.Play("BuyAreaSound"); // Yeni alan satın alındığında ses çalışır
                SaveManager.savemanagerInstance.SaveGame(); // Verileri kaydet
                this.enabled = false;   // Satın alma işlemi tamamlanınca kodu kapat
            }
        }
    }
}
