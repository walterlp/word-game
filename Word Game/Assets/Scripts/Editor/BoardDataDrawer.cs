using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(BoardData),false)]
[CanEditMultipleObjects]
[System.Serializable]
public class BoardDataDr : Editor
{
    private BoardData GameDataInstance => target as BoardData;
    private ReorderableList _datalist;
    private ReorderableList _datalistaux;

    private void OnEnable()
    {
        InitializeReordableList(ref _datalist, "SearchWords", "Searching Words");
        IniciandoLista(ref _datalistaux, "TraduzWords", "Traduzindo Words");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawColumnsRowsInputFields();
        EditorGUILayout.Space();
        ConvertToUpperButton();

        if (GameDataInstance.Board != null && GameDataInstance.Columns > 0 && GameDataInstance.Rows > 0)
            DrawboardTable();

        GUILayout.BeginHorizontal();
        
        ClearBoardButton();
        FillUpWithRandomLettersButton();
        
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();
        _datalist.DoLayoutList();
        EditorGUILayout.Space();
        _datalistaux.DoLayoutList();
        

        serializedObject.ApplyModifiedProperties();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(GameDataInstance);
        }
    }

    private void DrawColumnsRowsInputFields()
    {
        var columnsTemp = GameDataInstance.Columns;
        var rowsTemp = GameDataInstance.Rows;

        GameDataInstance.Columns = EditorGUILayout.IntField("Columns", GameDataInstance.Columns);
        GameDataInstance.Rows = EditorGUILayout.IntField("Rows", GameDataInstance.Rows);

        if((GameDataInstance.Columns != columnsTemp || GameDataInstance.Rows != rowsTemp) && GameDataInstance.Columns > 0 && GameDataInstance.Rows > 0)
        {
            GameDataInstance.CreateNewBoard();
        }
    }

    private void DrawboardTable()
    {
        var tableStyle = new GUIStyle("box");
        tableStyle.padding = new RectOffset(10,10,10,10);
        tableStyle.margin.left = 32;

        var headerColumnStyle = new GUIStyle();
        headerColumnStyle.fixedWidth = 35;

        var columnStyle = new GUIStyle();
        columnStyle.fixedWidth = 50;

        var rowStyle = new GUIStyle();
        rowStyle.fixedHeight = 25;
        rowStyle.fixedWidth = 40;
        rowStyle.alignment = TextAnchor.MiddleCenter;

        var textFieldStyle = new GUIStyle();
        textFieldStyle.normal.background = Texture2D.grayTexture;
        textFieldStyle.normal.textColor = Color.white;
        textFieldStyle.fontStyle = FontStyle.Bold;
        textFieldStyle.alignment = TextAnchor.MiddleCenter;

        EditorGUILayout.BeginHorizontal(tableStyle);
        for (var x = 0; x< GameDataInstance.Columns; x++)
        {
            EditorGUILayout.BeginVertical(x == -1 ? headerColumnStyle : columnStyle);
            for(var y = 0; y< GameDataInstance.Rows; y++)
            {
                if(x >= 0 && y >= 0)
                {
                    EditorGUILayout.BeginHorizontal(rowStyle);
                    var character = (string)EditorGUILayout.TextArea(GameDataInstance.Board[x].Row[y], textFieldStyle);
                    if(GameDataInstance.Board[x].Row[y].Length > 1)
                    {
                        character = GameDataInstance.Board[x].Row[y].Substring(0,1);
                    }

                    GameDataInstance.Board[x].Row[y] = character;
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();

    }
    
    private void InitializeReordableList(ref ReorderableList list, string propertyName, string listLabel)
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty(propertyName), true, true, true, true);
        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, listLabel);
        };
        var l = list;
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = l.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("Word"), GUIContent.none);


        };

    }
    private void IniciandoLista(ref ReorderableList list, string propertyName, string listLabel)
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty(propertyName), true, true, true, true);
        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, listLabel);
        };
        var l = list;
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = l.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("Traduzword"), GUIContent.none);


        };

    }
    private void ConvertToUpperButton()
    {
        if(GUILayout.Button("to Upper"))
        {
            for(var i=0;i< GameDataInstance.Columns; i++)
            {
                for(var j = 0; j < GameDataInstance.Rows; j++)
                {
                    var errorCounter = Regex.Matches(GameDataInstance.Board[i].Row[j], @"[a-z]").Count;

                    if (errorCounter > 0)
                        GameDataInstance.Board[i].Row[j] = GameDataInstance.Board[i].Row[j].ToUpper();
                }
            }

            foreach (var searchWord in GameDataInstance.SearchWords)
            {
                var errorCounter = Regex.Matches(searchWord.Word, @"[a-z]").Count;
                if(errorCounter > 0)
                {
                    searchWord.Word = searchWord.Word.ToUpper();
                }
            }
            foreach (var traduzWord in GameDataInstance.TraduzWords)
            {
                var errorCounter = Regex.Matches(traduzWord.Traduzword, @"[a-z]").Count;
                if (errorCounter > 0)
                {
                    traduzWord.Traduzword = traduzWord.Traduzword.ToUpper();
                }
            }
        }
    }

    private void ClearBoardButton()
    {
        if(GUILayout.Button("Clear Board"))
        {
            for(int i=0; i< GameDataInstance.Columns; i++)
            {
                for (int j =0; j< GameDataInstance.Rows; j++)
                {
                    GameDataInstance.Board[i].Row[j] = "";
                }
            }
        }
    }
    private void FillUpWithRandomLettersButton()
    {
        if (GUILayout.Button("Fill up with Random"))
        {
            for (int i = 0; i < GameDataInstance.Columns; i++)
            {
                for (int j = 0; j < GameDataInstance.Rows; j++)
                {
                    int errorCounter = Regex.Matches(GameDataInstance.Board[i].Row[j], @"[a-zA-Z]").Count;
                    string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    int index = UnityEngine.Random.Range(0,letters.Length);
                    if(errorCounter == 0 )
                    {
                        GameDataInstance.Board[i].Row[j] = letters[index].ToString();
                    }
                }
            }
        }
    }
}
