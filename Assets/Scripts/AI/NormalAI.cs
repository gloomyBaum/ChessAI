using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class NormalAI : ChessAICore
{
    public NormalAI(ChessBoard board, int maxCalcDepth = 2)
    {
        this.board = board;
        SetPieceValues(board.pieceList);
        moveStack = new Stack<ChessMove>();
        // set how many moves the AI should calculate ahead
        this.maxCalcDepth = maxCalcDepth;
    }
    /// <summary>
    /// Generates Move that consider Board Value and future Moves of the enemy.
    /// </summary>
    /// <returns></returns>
    public override ChessMove NewAIMove()
    {
        Minimax(maxCalcDepth, -10000000, 10000000, true);
        return bestMove;
    }
}
