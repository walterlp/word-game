using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameData currentGameData;
    //public int score;
    public Text scoreText; // Objeto de texto para exibir a pontua��o

    /*
    public void UpdateScore()
    {
        score = score + 10; // Voc� pode ajustar o valor da pontua��o conforme necess�rio
        currentGameData.score = currentGameData.score + score;
        UpdateScoreText(); // Chame o m�todo para atualizar o texto da pontua��o
    }*/

    public void UpdateScoreText()
    {
        scoreText.text = "Pontua��o: " + currentGameData.score.ToString(); // Atualiza o texto com o valor da pontua��o
    }
    public void UpScore(int tempo)
    {
        currentGameData.score = currentGameData.score + tempo;
        UpdateScoreText();
    }
}
