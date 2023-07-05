using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configuracao : MonoBehaviour
{
    public GameLevelData levelData;
    public GameData currentGameData;
    public Confirmando confirmando;

    
    public void abreJanela() {
        confirmando.gameObject.SetActive(true);
    }
    
    
    public void zerarSave()
    {
        DataSaver.ClearGameData(levelData);
        currentGameData.score = 0;
        currentGameData.valFase = 0;
        confirmando.gameObject.SetActive(false);
    }
    public void naoZera()
    {
        confirmando.gameObject.SetActive(false);
    }
}
