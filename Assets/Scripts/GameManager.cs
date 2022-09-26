using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
    public void GameExit()
    {
        SaveManager.savemanagerInstance.SaveGame(); // Verileri kaydet
        Application.Quit(); // Oyundan çýk
    }
}
