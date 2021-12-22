using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverNotification : MonoBehaviour
{

    public GameObject gameOverNoti;
    public GameObject continuarJogando;
    public GameLevelData gameLevelData;
    private bool tocamusica = true;
    private AudioSource _audiolose;

    void Start()
    {
        _audiolose = GetComponent<AudioSource>();

        continuarJogando.GetComponent<Button>().interactable = false;
        gameOverNoti.SetActive(false);
        tocamusica = true;


    }
    private void OnEnable()
    {
        GameEvents.OnGameOver += ShowGameOverNoti;
        
    }
    private void OnDisable()
    {
        GameEvents.OnGameOver -= ShowGameOverNoti;
        
    }
    private void ShowGameOverNoti()
    {
        gameOverNoti.SetActive(true);
        
        continuarJogando.GetComponent<Button>().interactable = true;

        if (SoundManager.instance.FundoestaMutado() == false && tocamusica == true)
        {
            _audiolose.Play();
            tocamusica = false;
        }
            
            //
    }
}
