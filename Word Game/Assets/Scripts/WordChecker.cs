using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WordChecker : MonoBehaviour
{

    public GameData currentGameData;
    public GameLevelData gameLevelData;
    

    private string _word;
    private int _assignedPoints = 0;
    private int _completedWords = 0;
    private Ray _rayUp, _rayDown;
    private Ray _rayRight, _rayLeft;
    private Ray _rayDiagonalRightUp, _rayDiagonalLeftDown;
    private Ray _rayDiagonalRightDown, _rayDiagonalLeftUp;
    private Ray _currentRay = new Ray();
    private Vector3 _rayStartPosition;
    private List<int> _correctSquareList = new List<int>();


    private void OnEnable()
    {
        GameEvents.OnCheckSquare += SquareSelected;
        GameEvents.OnClearSelection += ClearSelection;
        GameEvents.OnLoadNextLevel += loadNextGameLevel;
    }
    private void OnDisable()
    {
        GameEvents.OnCheckSquare -= SquareSelected;
        GameEvents.OnClearSelection -= ClearSelection;
        GameEvents.OnLoadNextLevel += loadNextGameLevel;
    }
    private void loadNextGameLevel()
    {
        SceneManager.LoadScene("GameCena");
    }


    void Start()
    {
        currentGameData.selectedBoardData.ClearData();

        _assignedPoints = 0;
        _completedWords = 0;


    }

   
    void Update()
    {
        if (_assignedPoints > 0 && Application.isEditor)
        {
            Debug.DrawRay(_rayUp.origin, _rayUp.direction *4 );
            Debug.DrawRay(_rayDown.origin, _rayDown.direction * 4);
            Debug.DrawRay(_rayRight.origin, _rayRight.direction * 4);
            Debug.DrawRay(_rayLeft.origin, _rayLeft.direction * 4);
           
            Debug.DrawRay(_rayDiagonalLeftUp.origin, _rayDiagonalLeftUp.direction * 4);
            Debug.DrawRay(_rayDiagonalRightUp.origin, _rayDiagonalRightUp.direction * 4);
            Debug.DrawRay(_rayDiagonalLeftDown.origin, _rayDiagonalLeftDown.direction * 4);
            Debug.DrawRay(_rayDiagonalRightDown.origin, _rayDiagonalRightDown.direction * 4);
        }
    }




    private void SquareSelected(string letter, Vector3 squarePosition, int squareIndex)
    {
        if(_assignedPoints == 0)
        {
            _rayStartPosition = squarePosition;
            _correctSquareList.Add(squareIndex);
            _word += letter;
            _rayUp = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(0f,1));
            _rayDown = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(0f, -1));
            _rayLeft = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(-1, 0f));
            _rayRight = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(1 , 0f));
            _rayDiagonalLeftUp = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(-1, 1));
            _rayDiagonalLeftDown = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(-1, -1));
            _rayDiagonalRightUp = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(1, 1));
            _rayDiagonalRightDown = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(1, -1));
        }
        else if(_assignedPoints == 1)
        {
            _correctSquareList.Add(squareIndex);
            _currentRay = SelectRay(_rayStartPosition, squarePosition);
            GameEvents.SelectSquareMethod(squarePosition);
            _word += letter;
             CheckWord();
        }
        else
        {
            if(isPointOnTheRay(_currentRay, squarePosition))
            {
                _correctSquareList.Add(squareIndex);
                GameEvents.SelectSquareMethod(squarePosition);
                _word += letter;
                CheckWord();
            }
        }
        _assignedPoints++;
        
        
    }

    private void CheckWord()
    {
        
            foreach (var searchingWord in currentGameData.selectedBoardData.SearchWords)
            {

                if (_word == searchingWord.Word && searchingWord.Achado == false)
                {
                    searchingWord.Achado = true;
                    GameEvents.CorrectWordMethod(_word, _correctSquareList);
                    _completedWords++;
                    _word = string.Empty;
                    _correctSquareList.Clear();
                    checkBoardCompleta();
                    
                    return;
                }
            }
        
    }


    private bool isPointOnTheRay(Ray currentRay, Vector3 point)
    {
        var hits = Physics.RaycastAll(currentRay,100.0f);

        for (int i=0; i< hits.Length; i++) {
            if (hits[i].transform.position == point) {
                return true;
            }
        }
        return false; 
    }

    private Ray SelectRay(Vector2 firstPosition, Vector2 secondPosition)
    {
        var direction = (secondPosition - firstPosition).normalized;
        float tolerance = 0.01f;
        if (Mathf.Abs(direction.x) < tolerance && Mathf.Abs(direction.y - 1f) < tolerance)
        {
            return _rayUp;
        }
        if (Mathf.Abs(direction.x) < tolerance && Mathf.Abs(direction.y - (-1f)) < tolerance)
        {
            return _rayDown;
        }
        if (Mathf.Abs(direction.x - (-1f)) < tolerance && Mathf.Abs(direction.y) < tolerance)
        {
            return _rayLeft;
        }
        if (Mathf.Abs(direction.x - 1f) < tolerance && Mathf.Abs(direction.y) < tolerance)
        {
            return _rayRight;
        }
        if (direction.x <  0f && direction.y > 0f)
        {
            return _rayDiagonalLeftUp;
        }
        if(direction.x < 0f && direction.y < 0f)
        {
            return _rayDiagonalLeftDown;
        }
        if (direction.x > 0f && direction.y > 0f)
        {
            return _rayDiagonalRightUp;
        }
        if (direction.x > 0f && direction.y < 0f)
        {
            return _rayDiagonalRightDown;
        }

        return _rayDown;
    }

    private void ClearSelection()
    {
        _assignedPoints = 0;
        _correctSquareList.Clear();
        _word = string.Empty;
    }
    private void checkBoardCompleta()
    {
        bool loadCategoria = false;
        if(currentGameData.selectedBoardData.SearchWords.Count == _completedWords)
        {
            //salvar o progresso do jogo
            var categoryName = currentGameData.selectedCategoryName;
            var currentBoardIndex = DataSaver.ReadCategoryCurrentIndexValues(categoryName);
            var nextBoardIndex = -1;
            var currentCategoryIndex = 0;
            bool readNlName = false;
            
            for (int ajudanteIndex=0; ajudanteIndex < gameLevelData.data.Count; ajudanteIndex++)
            {
                if (readNlName)
                {
                    nextBoardIndex = DataSaver.ReadCategoryCurrentIndexValues(gameLevelData.data[ajudanteIndex].categoryName);
                    readNlName = false;
                }
                if(gameLevelData.data[ajudanteIndex].categoryName == categoryName)
                {
                    readNlName = true;
                    currentCategoryIndex = ajudanteIndex;
                }
            }


            var currentLevelSize = gameLevelData.data[currentCategoryIndex].boardData.Count;
            if (currentBoardIndex < currentLevelSize)
                currentBoardIndex += 1;
            DataSaver.SaveCategory(categoryName, currentBoardIndex);
            //desbloquear proxima categoria
            if(currentBoardIndex >= currentLevelSize){
                currentCategoryIndex++;
                if(currentCategoryIndex < gameLevelData.data.Count){    //se não for a ultima categoria
                    categoryName = gameLevelData.data[currentCategoryIndex].categoryName;
                    currentBoardIndex = 0;
                    loadCategoria = true;

                    if (nextBoardIndex <= 0){
                        DataSaver.SaveCategory(categoryName, currentBoardIndex);
                    }

                }
                else{
                    GameEvents.FimJogoMethod();
                    //SceneManager.LoadScene("MenuSelecao"); //carregar tela de zeramento
                }
            }
            else
            {
                GameEvents.BoardCompletedMethod();
            }
            if (loadCategoria)
                GameEvents.UnlockNextCategoryMethod();
        }
    }
   
}
