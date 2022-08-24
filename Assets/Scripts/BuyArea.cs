using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyArea : MonoBehaviour
{
    [SerializeField] private GameObject farmerAndShopObject, buyObject;
    [SerializeField] private int cost;    // Satýn alýnacak öðenin maliyeti
    [SerializeField] private int currentMoney; // Ödenen para miktarý
    [SerializeField] private float progress;
    [SerializeField] private Image progressImage;
    [SerializeField] private TextMeshProUGUI areaText;

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
                this.enabled = false;   // Satýn alma iþlemi tamamlanýnca kodu kapat
            }
        }

    }
}
