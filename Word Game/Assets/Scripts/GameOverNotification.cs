using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverNotification : MonoBehaviour
{

    public GameObject gameOverNoti;
    public GameObject continuarJogando;
    public GameLevelData gameLevelData;

    void Start()
    {
        continuarJogando.GetComponent<Button>().interactable = false;
        gameOverNoti.SetActive(false);

        GameEvents.OnGameOver += ShowGameOverNoti;

    }
    private void OnDisable()
    {
        GameEvents.OnGameOver -= ShowGameOverNoti;
    }
    private void ShowGameOverNoti()
    {
        gameOverNoti.SetActive(true);
        
        continuarJogando.GetComponent<Button>().interactable = false;
    }
}
