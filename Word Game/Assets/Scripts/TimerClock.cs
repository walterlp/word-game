using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerClock : MonoBehaviour
{

    public GameData currentGameData;
    public Text timerText;

    private float _temporestante;
    private float _minutos;
    private float _segundos;
    private float _segundoAmenos;

    private bool _pausa;
    private bool _paraRelogio;

    void Start()
    {
        _paraRelogio = false;
        _pausa = false;
        _temporestante = currentGameData.selectedBoardData.timeSeconds;
        _segundoAmenos = _temporestante - 1f;

        GameEvents.OnBoardCompleted += ParaRelogio;
        GameEvents.OnUnlockNextCategory += ParaRelogio;
    }
    private void OnDisable()
    {
        GameEvents.OnBoardCompleted -= ParaRelogio;
        GameEvents.OnUnlockNextCategory -= ParaRelogio;
    }



    public void ParaRelogio()
    {
        _paraRelogio = true;
    }

    private void Update()
    {
        if(_paraRelogio == false)
            _temporestante -= Time.deltaTime;
        if(_temporestante <= _segundoAmenos)
            _segundoAmenos = _temporestante - 1f;
        
    }

    void OnGUI()
    {
        if(_pausa == false)
        {
            if(_temporestante > 0)
            {
                _minutos = Mathf.Floor(_temporestante / 60);
                _segundos = Mathf.RoundToInt(_temporestante % 60);

                timerText.text = _minutos.ToString("00") + ":" + _segundos.ToString("00");
            }
            else
            {
                _paraRelogio = true;
                AtivarGameOverGUI();
            }
        }
    }



    private void AtivarGameOverGUI()
    {
        GameEvents.GameOverMethod();
        _paraRelogio = true;

    }
}
