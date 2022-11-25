using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FimScript : MonoBehaviour
{
    private AudioSource _audi;
    public GameObject Fim;


    void Start(){
        _audi = GetComponent<AudioSource>();
        Fim.SetActive(false);

        GameEvents.OnFimJogo += OnFimJogo;
    }

    private void OnDisable()
    {
        GameEvents.OnFimJogo -= OnFimJogo;
    }


    private void OnFimJogo(){
        
        Fim.SetActive(true);
        if (SoundManager.instance.FundoestaMutado() == false)
            _audi.Play();
    }


}
