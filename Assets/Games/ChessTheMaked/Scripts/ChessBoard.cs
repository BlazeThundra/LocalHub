using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ChessTheMaked
{
[System.Serializable]
public class RowData
{
    public List<GameObject> columns = new List<GameObject>();
}


[System.Serializable]
public class Board
{
    public List<RowData> rows = new List<RowData>();
}


public class ChessBoard : MonoBehaviour
{
    [SerializeField] Board board;
    public GameObject selectedPiece;
    public Image attackIndicator;
    public Image moveIndicator;
    public string turn = "white";
    [SerializeField] Color whiteColor;
    public bool turnAttack = true;
    public bool turnMove = true;
    public bool queenAttacking;
    public bool kingAttacking;
    public bool alchemistAttacking;
    [SerializeField] public List<GameObject> whitePieces;
    [SerializeField] public List<GameObject> blackPieces;


    public Tuple<int, int> GetIndex(string squareName)
    {
        // Example: "A1" -> column = 'A', row = 1
        if (string.IsNullOrEmpty(squareName) || squareName.Length < 2)
        {
            Debug.LogError("Invalid square name: " + squareName);
            return null;
        }


        char colChar = squareName[0];               // 'A'–'H'
        int rowNum = int.Parse(squareName.Substring(1)); // 1–8


        // Convert to indices
        int colIndex = colChar - 'A';   // 'A'->0, 'B'->1, ...
        int rowIndex = rowNum;


        return new Tuple<int, int>(rowIndex, colIndex);
    }


    public GameObject GetSquareAt(int row, int col)
    {
        if (row >= 0 && row < board.rows.Count &&
            col >= 0 && col < board.rows[row].columns.Count)
        {
            return board.rows[row].columns[col];
        }
        return null;
    }


    public void ChangeTurn()
    {
        if (turn == "white" && (turnAttack == false && turnMove == false))
        {
            turn = "black";
            turnMove = true;
            turnAttack = true;
        }
        else if (turn == "black" && (turnAttack == false && turnMove == false))
        {
            turn = "white";
            turnMove = true;
            turnAttack = true;
         }
    }


    public void ResetHighlight()
    {
        foreach (Transform container in transform)
        {
            container.GetComponent<ClickSquare>().ResetHighlight();
        }
    }


    public void EndGame()
    {
     if(blackPieces.Count < 1)
     {
      SceneManager.LoadScene("CTMWhiteWin");
     }
     else if (whitePieces.Count < 1) { SceneManager.LoadScene("CTMBlackWin");}
    }


    void Start()
    {
        Camera.main.backgroundColor = Color.white;
    }


    void Update()
    {
        whitePieces.RemoveAll(p => p == null);
        blackPieces.RemoveAll(p => p == null);
        EndGame();
       
        if (Input.GetMouseButtonDown(1) && selectedPiece != null)
        {
            ResetHighlight();
        }


        if (!turnMove && !turnAttack)
            ChangeTurn();


        if (turn == "white")
        {
            Camera.main.backgroundColor = whiteColor;
        }
        else if (turn == "black")
        {
            Camera.main.backgroundColor = Color.black;
        }


        if (turnAttack == true)
        {
            attackIndicator.color = Color.red;
        }
        else
        {
            if (turn == "white")
            {
                attackIndicator.color = Color.black;
            }
            else
            {
                attackIndicator.color = Color.white;
            }
        }


        if (turnMove == true)
        {
            moveIndicator.color = Color.cyan;
        }
        else
        {
            if (turn == "white")
            {
                moveIndicator.color = Color.black;
            }
            else
            {
                moveIndicator.color = Color.white;
            }
        }
    }
}
}