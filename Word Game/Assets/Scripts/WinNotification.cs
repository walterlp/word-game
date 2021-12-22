using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WinNotification : MonoBehaviour
{
    public GameObject notiCation;
    private AudioSource _audiowin;
    private bool tocamus = true;

    void Start()
    {
        _audiowin = GetComponent<AudioSource>();
        notiCation.SetActive(false);
        tocamus = true;
    }

    private void OnEnable()
    {
        GameEvents.OnBoardCompleted += ShowWinNot;
    }

    private void OnDisable()
    {
        GameEvents.OnBoardCompleted -= ShowWinNot;
    }

   private void ShowWinNot()
    {
        notiCation.SetActive(true);
        
        if(SoundManager.instance.FundoestaMutado() == false && tocamus == true)
        {
            _audiowin.Play();
            tocamus = false;
        }
            
    }

    public void LoadNextLevel()
    {
        GameEvents.LoadNextLevelMethod();
    }
}
