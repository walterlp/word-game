using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Events;

public class SearchingWord : MonoBehaviour
{

    public Text displayedText;
    public Image crossLine;

    private string _word;
    
    

  

    void Start()
    {
        
    }
   

    private void OnEnable()
    {
        GameEvents.OnCorrectWord += CorrectWord;
    }
    private void OnDisable()
    {
        GameEvents.OnCorrectWord -= CorrectWord;
    }

    public void SetWord(string word , string traduz)
    {
        _word = word;
        displayedText.text = traduz;
    }
    
    private void CorrectWord(string word, List<int> squareIndexes)
    {
        if(word == _word)
        {
            crossLine.gameObject.SetActive(true);
        }
    }
    
}
