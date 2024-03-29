using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeachingWordsList : MonoBehaviour
{
    public GameData currentGameData;
    public GameObject searchingWordPrefab;
    public float offset = 0.0f;
    public int maxColumns = 5;
    public int maxRows = 4;

    private int _columns = 2;
    private int _rows = 2;
    private int _wordsNumber;
  
    private List<GameObject> _words = new List<GameObject>();
    //private List<GameObject> _twords = new List<GameObject>();

    private void Start()
    {
        _wordsNumber = currentGameData.selectedBoardData.SearchWords.Count;

        if (_wordsNumber < _columns)
            _rows = 1;
        else
            CalculateColumnsAndRowsNumber();

        CreateWordObjects();
        setWordsPosition();

    }
    
    private void CalculateColumnsAndRowsNumber()
    {
        do
        {
            _columns++;
            _rows = _wordsNumber / _columns;
        } while (_rows >= maxRows);

        if (_columns > maxColumns)
        {
            _columns = maxColumns;
            _rows = _wordsNumber / _columns;

        }

    }


    private bool TryIncreaseColumnsNumber()
    {
        _columns++;
        _rows = _wordsNumber / _columns;

        if (_columns > maxColumns)
        {
            _columns = maxColumns;
            _rows = _wordsNumber / _columns;
        }

        if (_wordsNumber % _columns > 0)
            _rows++;
        return true;

    }

    private void CreateWordObjects()
    {
        var squareScale = GetSquareScale(new Vector3(1f, 1f, 0.1f));
        for (var index = 0; index < _wordsNumber; index++)
        {
            _words.Add(Instantiate(searchingWordPrefab) as GameObject);
            _words[index].transform.SetParent(this.transform);
            _words[index].GetComponent<RectTransform>().localScale = squareScale;
            _words[index].GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
            _words[index].GetComponent<SearchingWord>().SetWord(currentGameData.selectedBoardData.SearchWords[index].Word
                    ,currentGameData.selectedBoardData.TraduzWords[index].Traduzword);
        }
    }
    private Vector3 GetSquareScale(Vector3 defaultScale)
    {
        var finalScale = defaultScale;
        var adjustment = 0.01f;

        while (ShouldScaleDown(finalScale))
        {
            finalScale.x -= adjustment;
            finalScale.y -= adjustment;

            if (finalScale.x <= 0 || finalScale.y <= 0)
            {
                finalScale.x = adjustment;
                finalScale.y = adjustment;

                return finalScale;
            }

        }
        return finalScale;

    }
    private bool ShouldScaleDown(Vector3 targetScale)
    {
        var squareRect = searchingWordPrefab.GetComponent<RectTransform>();
        var parentRect = this.GetComponent<RectTransform>();

        var squareSize = new Vector2(0f, 0f);

        squareSize.x = squareRect.rect.width * targetScale.x + offset;
        squareSize.y = squareRect.rect.height * targetScale.y + offset;

        var totalSquareHeight = squareSize.y * _rows;

        //ter certeza que todos os quadrados se encaixam na area do retangulo pai
        if (totalSquareHeight > parentRect.rect.height)
        {
            while (totalSquareHeight > parentRect.rect.height)
            {
                if (TryIncreaseColumnsNumber())
                    totalSquareHeight = squareSize.y * _rows;
                else
                    return true;
            }
        }

        var totalSquareWidth = squareSize.x * _columns;

        if (totalSquareWidth > parentRect.rect.width)
            return true;

        return false;

    }

    private void setWordsPosition()
    {
        var squareRect = _words[0].GetComponent<RectTransform>();
        var wordOffSet = new Vector2(squareRect.rect.width * squareRect.transform.localScale.x + offset,
            squareRect.rect.height * squareRect.transform.localScale.y + offset);

        int columnNumber = 0;
        int rowNumber = 0;
        var startPosition = GetFirstSquarePosition();
        foreach (var word in _words)
        {
            if (columnNumber + 1 > _columns)
            {
                columnNumber = 0;
                rowNumber++;
            }

            var positionX = startPosition.x + wordOffSet.x * columnNumber;
            var positionY = startPosition.y - wordOffSet.y * rowNumber;

            word.GetComponent<RectTransform>().localPosition = new Vector2(positionX, positionY);
            columnNumber++;
        }



    }
    private Vector2 GetFirstSquarePosition()
    {
        var startPosition = new Vector2(0f, transform.position.y);
        var squareRect = _words[0].GetComponent<RectTransform>();
        var parentRect = this.GetComponent<RectTransform>();
        var squareSize = new Vector2(0f, 0f);


        squareSize.x = squareRect.rect.width * squareRect.transform.localScale.x + offset;
        squareSize.y = squareRect.rect.height * squareRect.transform.localScale.y + offset;

        //ter certeza de que estar�o no centro

        var defCenter = (parentRect.rect.width - (squareSize.x * _columns)) / 2;

        startPosition.x = ((parentRect.rect.width - squareSize.x) / 2) * (-1);
        startPosition.x += defCenter;
        startPosition.y = (parentRect.rect.height - squareSize.y) / 2;


        return startPosition;

    }



}

