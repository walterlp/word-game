using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUtility : MonoBehaviour
{

    
    //transi��o de telas com base no nome
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        
    }


    //encerrando aplica��o
    public void ExitApp()
    {
        Application.Quit();
    }

    public void MuteToggleBackGroundMusic()
    {
        SoundManager.instance.MudaSomFundo();
    }
    public void MuteToggleSoundFx()
    {
        SoundManager.instance.MudaSomEfeito();
    }
}
