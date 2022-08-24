using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyArea : MonoBehaviour
{
    [SerializeField] private GameObject farmerAndShopObject, buyObject;
    [SerializeField] private int cost;    // Sat�n al�nacak ��enin maliyeti
    [SerializeField] private int currentMoney; // �denen para miktar�
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
                // E�er sat�n alma tamamland�ysa
                buyObject.SetActive(false); // Sat�n alma triggerini pasif yapar
                farmerAndShopObject.SetActive(true);    // Sat�n al�nan ��eyi aktif hale getir
                this.enabled = false;   // Sat�n alma i�lemi tamamlan�nca kodu kapat
            }
        }

    }
}
