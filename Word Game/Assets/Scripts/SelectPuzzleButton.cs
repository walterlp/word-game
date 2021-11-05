using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectPuzzleButton : MonoBehaviour
{
    public GameData gameData;
    public GameLevelData levelData;
    public Text categoryText;
    public Image progressBarFilling;


    private string gameSceneName = "GameCena";

    private bool _levelLocked;

    void Start()
    {
        _levelLocked = false;
        var button = GetComponent<Button>();
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
        UpdateButtonInfo();
        if (_levelLocked)
        {
            button.interactable = false;
        }
        else {
            button.interactable = true;
        }

        
    }

    
    void Update()
    {
        
    }



    private void OnButtonClick()
    {
        gameData.selectedCategoryName = gameObject.name;
        SceneManager.LoadScene(gameSceneName);
    }

    private void UpdateButtonInfo()
    {
        var currentIndex = -1;
        var totalBoards = 0;

        foreach (var data in levelData.data)
        {
                if (data.categoryName == gameObject.name)
                {
                    currentIndex = DataSaver.ReadCategoryCurrentIndexValues(gameObject.name);
                    totalBoards = data.boardData.Count;

                    if (levelData.data[0].categoryName == gameObject.name && currentIndex < 0)
                    {
                        DataSaver.SaveCategory(levelData.data[0].categoryName, 0);
                        currentIndex = DataSaver.ReadCategoryCurrentIndexValues(gameObject.name);
                        totalBoards = data.boardData.Count;
                    }
                }
        }
        if(currentIndex == -1)
            _levelLocked = true;
        categoryText.text = _levelLocked ? string.Empty : (currentIndex.ToString() + "/" + totalBoards.ToString());
        progressBarFilling.fillAmount = (currentIndex > 0 && totalBoards > 0) ? ((float) currentIndex / (float) totalBoards) : 0f;
        /*checando se tem algum desafio feito se tiver vai pegar a % para a barra de progresso e se n a progress será 0f = fim */
    }
}
