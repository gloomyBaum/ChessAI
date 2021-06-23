using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    public static SessionManager instance;

    private bool isPlayerTurn;
    private ChessPiece selectedPiece;

    ChessBoard board;
    ChessAICore ai;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        isPlayerTurn = true;
        selectedPiece = null;
    }
    /// <summary>
    /// Start new Game with AI Settings
    /// </summary>
    /// <param name="setting"></param>
    public void SetupNewGame(ChessAICore.AISetting setting)
    {
        board = GameSetup.instance.Setup();
        switch (setting)
        {
            case ChessAICore.AISetting.Random:
                ai = new RandomAI(board);
                break;
            case ChessAICore.AISetting.Easy:
                ai = new EasyAI(board);
                break;
            case ChessAICore.AISetting.Normal:
                ai = new NormalAI(board);
                break;
            case ChessAICore.AISetting.Hard:
                ai = new HardAI(board, GameManager.Instance.difficulty);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// Handle Mouse Input if clicked on a Tile
    /// </summary>
    /// <param name="position">Position of the Tile clicked</param>
    public void HandleMouseClick(Vector2Int position)
    {
        if(isPlayerTurn)
        {
            if (CheckIfFriendly(position))
            {
                SelectPiece(position);
            }
            else if(selectedPiece)
            {       
                ChessMove move = board.GetValidMove(selectedPiece, position);
                if (move != null)
                {
                    PerformMove(move);
                    board.CancelHighlights();
                }
                else
                {
                    selectedPiece = null;
                    board.CancelHighlights();
                }                
            }
        }
    }
    private bool CheckIfFriendly(Vector2Int position)
    {
        ChessPiece clickedPiece = board.GetChessPiece(position);
        return (clickedPiece && clickedPiece.chessPieceColor == ChessPiece.Color.White == isPlayerTurn && !clickedPiece.fakeKilled);
    }
    private void SelectPiece(Vector2Int position)
    {
        //Delete old Highlights
        if (selectedPiece)
            board.CancelHighlights();
        selectedPiece = board.GetChessPiece(position);
        //Highlight all PossibleMoves for the selected piece
        board.HighlightAttackTiles(selectedPiece);
    }
    /// <summary>
    /// Perform the given Move. Wait 1 second before AI takes his Turn.
    /// </summary>
    /// <param name="move"></param>
    /// <returns></returns>
    private int PerformMove(ChessMove move)
    {
        if (move.targetPiece)
        {
            board.DestroyChessPiece(move.targetPiece);
            // check if king is killed
            if (move.targetPiece.chessPieceType == ChessPiece.Type.King)
            {
                board.MoveChessPiece(move.piece, move.destination);
                EndGame();
                return 0;
            }     
        }
        board.MoveChessPiece(move.piece, move.destination);

        isPlayerTurn = !isPlayerTurn;
        if(!isPlayerTurn)
        {            
            StartCoroutine(MakeAIMove());
        }
        return 0;
    }
    private IEnumerator MakeAIMove()
    {
        yield return new WaitForSeconds(1f);
        PerformMove(ai.NewAIMove());
    }
    private void EndGame()
    {
        if (isPlayerTurn)
        {
            StartCoroutine(EndScreen.instance.HandleWin());
        }           
        else
        {
            StartCoroutine(EndScreen.instance.HandleLose());
        }           
    }
}
