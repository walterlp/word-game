using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataSelector : MonoBehaviour
{

    public GameData currentGameData;
    public GameLevelData levelData;

    void Awake()
    {
        selectSequenciaTabela();
    }


 
    private void selectSequenciaTabela()
    {
        foreach (var data in levelData.data)
        {
            if(data.categoryName == currentGameData.selectedCategoryName)
            {
                var boardIndex = 0; //TODO this need to be read from the external file.

                if (boardIndex < data.boardData.Count)
                {
                    currentGameData.selectedBoardData = data.boardData[boardIndex];
                }
                else
                {
                    var randomIndex = Random.Range(0, data.boardData.Count);
                    currentGameData.selectedBoardData = data.boardData[randomIndex];
                }
            }
        }
    }

}
