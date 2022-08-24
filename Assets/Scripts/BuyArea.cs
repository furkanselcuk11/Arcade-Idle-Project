using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyArea : MonoBehaviour
{
    [SerializeField] private GameObject farmerAndShopObject, buyObject;
    [SerializeField] private float cost;    // Satýn alýnacak öðenin maliyeti
    [SerializeField] private float currentMoney; // Ödenen para miktarý
    [SerializeField] private float progress;
    [SerializeField] private Image progressImage;

    public void Buy(int valueMoney)
    {
        currentMoney += valueMoney;
        progress = currentMoney / cost;
        progressImage.fillAmount = progress;
        if (progress >= 1)
        {
            // Eðer satýn alma tamamlandýysa
            buyObject.SetActive(false); // Satýn alma triggerini pasif yapar
            farmerAndShopObject.SetActive(true);    // Satýn alýnan öðeyi aktif hale getir
            this.enabled = false;   // Satýn alma iþlemi tamamlanýnca kodu kapat
        }
    }
}
