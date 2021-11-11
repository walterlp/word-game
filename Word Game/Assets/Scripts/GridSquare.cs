using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquare : MonoBehaviour
{
    
    public int SquareIndex { get; set;}
    private AlphabetData.LetterData _normalLetterData;
    private AlphabetData.LetterData _selectedLetterData;
    private AlphabetData.LetterData _correctLetterData;
    

    private SpriteRenderer _displayImage;

    private bool _selected;
    private bool _clicked;
    private bool _correct;
    
    private int _index = -1;
    


    public void setIndex(int index)
    {
        _index = index;
    }
    public int GetIndex()
    {
        return _index;
    }

    void Start()
    {
        _selected = false;
        _clicked = false;
        _correct = false;
        _displayImage = GetComponent<SpriteRenderer>();
        

    }

    private void OnEnable()
    {
        GameEvents.OnEnableSquareSelection += OnEnableSquareSelection;
        GameEvents.OnDisableSquareSelection += OnDisableSquareSelection;
        GameEvents.OnSelectSquare += OnSelectSquare;
        GameEvents.OnCorrectWord += CorrectWord;
    }

    private void OnDisable()
    {
        GameEvents.OnEnableSquareSelection -= OnEnableSquareSelection;
        GameEvents.OnDisableSquareSelection -= OnDisableSquareSelection;
        GameEvents.OnSelectSquare -= OnSelectSquare;
        GameEvents.OnCorrectWord -= CorrectWord;
    }

    private void CorrectWord(string word ,List<int> squareIndexes)
    {
        if (_selected && squareIndexes.Contains(_index))
        {
            _correct = true;
            _displayImage.sprite = _correctLetterData.image;
        }     
        _selected = false;
        _clicked = false;   
    }

    public void OnEnableSquareSelection()
    {
        _clicked = true;
        _selected = false;
    }
    public void OnDisableSquareSelection()
    {
        _selected = false;
        _clicked = false;
        if (_correct == true)
            _displayImage.sprite = _correctLetterData.image;
        else
            _displayImage.sprite = _normalLetterData.image;

    }
    public void OnSelectSquare(Vector3 position)
    {
        if (this.gameObject.transform.position == position)
        {
            _displayImage.sprite = _selectedLetterData.image;
        }
    }
    public void SetSprite(AlphabetData.LetterData normalLetterData, AlphabetData.LetterData selectedLetterData, AlphabetData.LetterData correctLetterData)
    {
        _normalLetterData = normalLetterData;
        _selectedLetterData = selectedLetterData;
        _correctLetterData = correctLetterData;

        GetComponent<SpriteRenderer>().sprite = _normalLetterData.image;
    }



    private void OnMouseDown()
    {
        OnEnableSquareSelection();
        GameEvents.EnableSquareSelectionMethod();
        CheckSquare();
        _displayImage.sprite = _selectedLetterData.image;
        
    }

    private void OnMouseEnter()
    {
        CheckSquare();
    }

    private void OnMouseUp()
    {
            GameEvents.ClearSelectionMethod();
            GameEvents.DisebleSquareSelectionMethod();
    }


    public void CheckSquare()
    {
        if(_selected == false && _clicked == true)
        {
            _selected = true;
            GameEvents.CheckSquareMethod(_normalLetterData.letter, gameObject.transform.position, _index);
        }
    }
}
