using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configuracao : MonoBehaviour
{
    public GameLevelData levelData;
    

    public void zerarSave()
    {
        DataSaver.ClearGameData(levelData);
        
    }
    
}
