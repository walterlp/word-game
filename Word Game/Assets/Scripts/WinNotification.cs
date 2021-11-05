using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinNotification : MonoBehaviour
{
    public GameObject notiCation;


    void Start()
    {
        notiCation.SetActive(false);
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
    }

    public void LoadNextLevel()
    {
        GameEvents.LoadNextLevelMethod();
    }
}
