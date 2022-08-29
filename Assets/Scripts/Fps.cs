using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Fps : MonoBehaviour
{
    public float fps;
    public TextMeshProUGUI fpsText;
    public float fpsSecond=1f;
    void Start()
    {
        //InvokeRepeating(nameof(GetFPS), 1, 1);
        StartCoroutine(nameof(GetFPS));
    }
    IEnumerator GetFPS()
    {
        while (true)
        {
            fps = (int)(1f / Time.unscaledDeltaTime);
            fpsText.text = fps + " fps";
            yield return new WaitForSeconds(fpsSecond);
        }        
    }
}
