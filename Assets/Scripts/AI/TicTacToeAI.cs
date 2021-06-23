using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeAI : TicTacToeAICore
{
    public TicTacToeAI(int maxCalcDepth = 8)
    {
        this.maxCalcDepth = maxCalcDepth;
        moveStack = new Stack<TMove>();
    }
    public override TMove NewAIMove()
    {
        maxCalcDepth = TSessionManager.instance.GetPossibleMoves().Count;
        Minimax(maxCalcDepth, -10000000, 10000000, true);
        return bestMove;
    }
}
