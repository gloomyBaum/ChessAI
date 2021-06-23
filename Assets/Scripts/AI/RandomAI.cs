using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAI : ChessAICore
{
    public RandomAI(ChessBoard board)
    {
        this.board = board;
    }
    /// <summary>
    /// Generates a random Move out of Move-Pool
    /// </summary>
    /// <returns></returns>
    public override ChessMove NewAIMove()
    {
        List<ChessMove> moveList = board.GetAllPossibleMovesOfColor(ChessPiece.Color.Black);
        int randomIndex = UnityEngine.Random.Range(0, moveList.Count);
        return moveList[randomIndex];
    }
}
