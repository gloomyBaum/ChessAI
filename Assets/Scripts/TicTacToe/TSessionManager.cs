using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSessionManager : MonoBehaviour
{
    public static TSessionManager instance;
    private TicTacToeAI ai;
    private List<Cell> cellList;
    private bool playerTurn;
    private bool endGame;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space:");
            foreach (TMove move in GetPossibleMoves())
            {
                Debug.Log(move.cell.pos);
            }
        }
    }
    public void SetUpNewGame()
    {
        cellList = new List<Cell>();
        playerTurn = true;
        ai = new TicTacToeAI();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GameObject go = Instantiate(Resources.Load("TicTacToe/Prefabs/Field")) as GameObject;
                go.transform.position = new Vector3(i, j, 0);
                go.transform.parent = GameObject.Find("Board").transform;
                Cell cell = go.GetComponent<Cell>();
                cell.pos = new Vector2Int(i, j);
                cellList.Add(cell);
            }
        }
        
    }
    public void HandleClick(Cell cell)
    {
        if(cell.fieldState == Cell.State.Free && !endGame)
        {
            if (playerTurn)
            {
                MakePlayerMove(new TMove(cell));
            }            
        }
    }
    private void MakePlayerMove(TMove move)
    {
        move.cell.SetX();
        CheckForEndGame();
        playerTurn = !playerTurn;
        // Initiate AI Move
        if(!endGame)
            CalculateAIMove();
    }
    private void CalculateAIMove()
    {
        MakeAIMove(ai.NewAIMove());
    }
    private void MakeAIMove(TMove move)
    {
        move.cell.SetO();
        CheckForEndGame();
        playerTurn = !playerTurn;
    }
    private void CheckForEndGame()
    {
        if (ReturnWinner() == 0)
        {
            endGame = true;
            StartCoroutine(EndScreen.instance.HandleDraw());
                   
        }
        else if (ReturnWinner() == 1)
        {
            endGame = true;
            StartCoroutine(EndScreen.instance.HandleLose());
        }
        else if (ReturnWinner() == - 1)
        {
            endGame = true;
            StartCoroutine(EndScreen.instance.HandleWin());
        }
    } 
    /// <summary>
    /// check for winner
    /// </summary>
    /// <returns>-1 = Player, 0 = Draw, 1 = AI</returns>
    public int ReturnWinner()
    {
        //Cell to Matrix
        int[,] matrix = new int[3,3];
        //check for draw
        bool isFull = true;

        foreach (Cell cell in cellList)
        {
            if (cell.fieldState == Cell.State.Free)
            {
                isFull = false;
            }
            else if (cell.fieldState == Cell.State.X)
            {
                matrix[cell.pos.x, cell.pos.y] = -1;
            }
            else if (cell.fieldState == Cell.State.O)
            {
                matrix[cell.pos.x, cell.pos.y] = 1;
            }
        }
        if (CheckWinCondition(matrix) == 100 && isFull )
            return 0;
        return CheckWinCondition(matrix);
    }

    // check every possible combination
    private int CheckWinCondition(int[,] matrix)
    {
        //straight
        if (matrix[0, 0] == matrix[1, 0] && matrix[1, 0]  == matrix[2, 0] && matrix[2, 0] != 0)
        {
            return matrix[0, 0];
        }
        if (matrix[0, 1] == matrix[1, 1] && matrix[1, 1] == matrix[2, 1] && matrix[2, 1] != 0)
        {
            return matrix[0, 1];
        }
        if (matrix[0, 2] == matrix[1, 2] && matrix[1, 2] == matrix[2, 2] && matrix[2, 2] != 0)
        {
            return matrix[0, 2];
        }
        if (matrix[0, 0] == matrix[0, 1] && matrix[0, 1] == matrix[0, 2] && matrix[0, 2] != 0)
        {
            return matrix[0, 0];
        }
        if (matrix[1, 0] == matrix[1, 1] && matrix[1, 1] == matrix[1, 2] && matrix[1, 2] != 0)
        {
            return matrix[1, 0];
        }
        if (matrix[2, 0] == matrix[2, 1] && matrix[2, 1] == matrix[2, 2] && matrix[2, 2] != 0)
        {
            return matrix[2, 0];
        }
        //dia
        if (matrix[0, 0] == matrix[1, 1] && matrix[1, 1] == matrix[2, 2] && matrix[2, 2] != 0)
        {
            return matrix[0, 0];
        }
        if (matrix[0, 2] == matrix[1, 1] && matrix[1, 1] == matrix[2, 0] && matrix[2, 0] != 0)
        {
            return matrix[2, 0];
        }
        return 100;
    }
    public Cell GetCell(Vector2Int pos)
    {
        foreach(Cell cell in cellList)
        {
            if (cell.pos == pos)
                return cell;
        }
        return null;
    }
    public List<TMove> GetPossibleMoves()
    {
        List<TMove> list = new List<TMove>();
        foreach(Cell cell in cellList)
        {
            if (cell.fieldState == Cell.State.Free)
                list.Add(new TMove(cell));
        }
        return list;
    }
}
