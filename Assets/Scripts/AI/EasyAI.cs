using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EasyAI : ChessAICore
{
    public EasyAI(ChessBoard board)
    {
        this.board = board;
        SetPieceValues(board.pieceList);
        moveStack = new Stack<ChessMove>();
    }
    public override ChessMove NewAIMove()
    {
        int moveValue = -100000000;
        ChessMove bestMove = null;
        List<ChessMove> moveList = board.GetAllPossibleMovesOfColor(ChessPiece.Color.Black);
        moveList = moveList.OrderBy(a => Guid.NewGuid()).ToList();
        foreach (ChessMove move in moveList)
        {
            PerformFakeMove(move);
            int newMoveValue = EvalBoard();
            if (newMoveValue >= moveValue)
            {
                moveValue = newMoveValue;
                bestMove = move;
            }
            UndoFakeMove();
        }
        return bestMove;
    }
}
