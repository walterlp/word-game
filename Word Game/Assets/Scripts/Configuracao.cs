using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configuracao : MonoBehaviour
{
    public GameLevelData levelData;

    public Confirmando confirmando;

    
    public void abreJanela() {
        confirmando.gameObject.SetActive(true);
    }
    
    
    public void zerarSave()
    {
        DataSaver.ClearGameData(levelData);
        confirmando.gameObject.SetActive(false);
    }
    public void naoZera()
    {
        confirmando.gameObject.SetActive(false);
    }
}
