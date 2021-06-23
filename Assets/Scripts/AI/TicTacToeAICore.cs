using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TicTacToeAICore : IAI<TMove, TMove>
{
    protected Stack<TMove> moveStack;
    protected TMove bestMove;
    protected int value = 0;
    protected int maxCalcDepth;

    /// <summary>
    /// Calculates current Board State
    /// </summary>
    /// <returns>-1 = Player, 0 = Draw, 1 = AI wins</returns>
    public int EvalBoard()
    {
        return TSessionManager.instance.ReturnWinner();
    }

    public int Minimax(int depth, int alpha, int beta, bool isMaximizing)
    {
        int res = EvalBoard();
        // recursion anchor
        if (res != 100)
        {
            // return Board Value
            return res;
        }
        // currently maximizing player (AI)
        if (isMaximizing)
        {
            // has to be sufficient big negative number
            int bestMoveValue = -1000000;
            // List of all possible Moves of current Player
            List<TMove> moveList = TSessionManager.instance.GetPossibleMoves();
            // iterate over all possible moves
            foreach (TMove move in moveList)
            {
                // only to simulate move
                move.cell.fieldState = Cell.State.O;
                PerformFakeMove(move);
                // recursive call
                value = Minimax(depth - 1, alpha, beta, false);
                // undo simulated move
                UndoFakeMove();
                // check if move gives better value
                if (value > bestMoveValue)
                {
                    bestMoveValue = value;
                    // set bestMove if algorithm is at its root
                    if (depth == maxCalcDepth)
                    {
                        bestMove = move;
                    }
                    // set alpha
                    alpha = bestMoveValue;
                }
                // alpha - beta pruning
                if (value >= beta)
                {
                    break;
                }
            }
            return alpha;
        }
        // currently minimizing player
        else
        {
            // value has to be bigger than the biggest possible value of EvalBoard
            int bestMoveValue = 1000000;
            List<TMove> moveList = TSessionManager.instance.GetPossibleMoves();
            foreach (TMove move in moveList)
            {
                move.cell.fieldState = Cell.State.X;
                PerformFakeMove(move);
                value = Minimax(depth - 1, alpha, beta, true);
                UndoFakeMove();
                // check if move gives worse value
                if (value < bestMoveValue)
                {
                    bestMoveValue = value;
                    // set beta
                    beta = bestMoveValue;
                }
                // alpha - beta pruning
                if (value <= alpha)
                {
                    break;
                }
            }
            return beta;
        }
    }

    public virtual TMove NewAIMove()
    {
        return null;
    }

    public void PerformFakeMove(TMove move)
    {
        moveStack.Push(move);        
    }

    public void UndoFakeMove()
    {
        TMove move = moveStack.Pop();
        move.cell.fieldState = Cell.State.Free;
    }
}
