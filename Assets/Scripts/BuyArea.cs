using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyArea : MonoBehaviour
{
    [SerializeField] private GameObject farmerAndShopObject, buyObject;
    [SerializeField] private float cost;    // Sat�n al�nacak ��enin maliyeti
    [SerializeField] private float currentMoney; // �denen para miktar�
    [SerializeField] private float progress;
    [SerializeField] private Image progressImage;

    public void Buy(int valueMoney)
    {
        currentMoney += valueMoney;
        progress = currentMoney / cost;
        progressImage.fillAmount = progress;
        if (progress >= 1)
        {
            // E�er sat�n alma tamamland�ysa
            buyObject.SetActive(false); // Sat�n alma triggerini pasif yapar
            farmerAndShopObject.SetActive(true);    // Sat�n al�nan ��eyi aktif hale getir
            this.enabled = false;   // Sat�n alma i�lemi tamamlan�nca kodu kapat
        }
    }
}
