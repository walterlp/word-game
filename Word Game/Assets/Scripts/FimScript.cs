using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FimScript : MonoBehaviour
{
    private AudioSource _audi;
    public GameObject Fim;
    private ScoreManager ScoreManager;

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

        ScoreManager = GameObject.Find("FinalScoreManager").GetComponent<ScoreManager>();

        ScoreManager.FinalScoreText();

        if (SoundManager.instance.FundoestaMutado() == false) 
            _audi.Play();
            
        
    }


}
