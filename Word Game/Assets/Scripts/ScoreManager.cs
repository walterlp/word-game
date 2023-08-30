using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameData currentGameData;
    //public int score;
    public Text scoreText; // Objeto de texto para exibir a pontuação

    /*
    public void UpdateScore()
    {
        score = score + 10; // Você pode ajustar o valor da pontuação conforme necessário
        currentGameData.score = currentGameData.score + score;
        UpdateScoreText(); // Chame o método para atualizar o texto da pontuação
    }*/

    public void UpdateScoreText()
    {
        scoreText.text = "Pontuação: " + PlayerPrefs.GetInt("score"); // Atualiza o texto com o valor da pontuação
    }
    public void UpScore(int tempo)
    {
        currentGameData.score = PlayerPrefs.GetInt("score");
        currentGameData.score = currentGameData.score + tempo;
        
        PlayerPrefs.SetInt("score",currentGameData.score);

        UpdateScoreText();
        
    }
    public void FinalScoreText()
    {
        scoreText.text = "Pontuação Final: " + PlayerPrefs.GetInt("score"); // Atualiza o texto com o valor da pontuação
    }
}
