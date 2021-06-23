using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ChessAICore : IAI<ChessMove, ChessMove>
{
    protected ChessBoard board;
    protected Stack<ChessMove> moveStack;
    protected ChessMove bestMove;
    protected int value = 0;
    protected int maxCalcDepth;
    public enum AISetting
    {
        Random,
        Easy,
        Normal,
        Hard
    }
    public virtual ChessMove NewAIMove()
    {
        return null;
    }

    /// <summary>
    /// Set Values of pieces
    /// pawn = 100
    /// knight = 320
    /// bishop 330
    /// rook = 500
    /// queen = 900
    /// king = 20000
    /// </summary>
    /// <param name="chessPieceList"></param>
    public void SetPieceValues(Dictionary<Vector2Int, ChessPiece> pieceList)
    {
        foreach (ChessPiece piece in pieceList.Values)
        {
            switch (piece.chessPieceType)
            {
                case ChessPiece.Type.Rook:
                    piece.pieceValue = 500;
                    break;
                case ChessPiece.Type.Knight:
                    piece.pieceValue = 320;
                    break;
                case ChessPiece.Type.Bishop:
                    piece.pieceValue = 330;
                    break;
                case ChessPiece.Type.Queen:
                    piece.pieceValue = 900;
                    break;
                case ChessPiece.Type.King:
                    piece.pieceValue = 20000;
                    break;
                case ChessPiece.Type.Pawn:
                    piece.pieceValue = 100;
                    break;
                default:
                    break;
            }
        }
    }
    public virtual int EvalBoard()
    {
        int value = 0;
        foreach (ChessPiece piece in board.pieceList.Values)
        {
            if (!piece.fakeKilled)
            {
                if (piece.chessPieceColor == ChessPiece.Color.Black)
                {
                    value += piece.pieceValue;
                }
                else
                {
                    value += (piece.pieceValue * -1);
                }
            }
        }
        return value;
    }

    public int Minimax(int depth, int alpha, int beta, bool isMaximizing)
    {
        // recursion anchor
        if (depth <= 0)
        {
            // return Board Value
            return EvalBoard();
        }
        // currently maximizing player (AI)
        if (isMaximizing)
        {
            // has to be sufficient big negative number
            int bestMoveValue = -1000000;
            // List of all possible Moves of current Player
            List<ChessMove> moveList =
                board.GetAllPossibleMovesOfColor(ChessPiece.Color.Black);
            // shuffle List to prevent move repetition
            moveList = moveList.OrderBy(a => Guid.NewGuid()).ToList();
            // iterate over all possible moves
            foreach (ChessMove move in moveList)
            {
                // only to simulate move
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
            List<ChessMove> moveList =
                board.GetAllPossibleMovesOfColor(ChessPiece.Color.White);
            moveList = moveList.OrderBy(a => Guid.NewGuid()).ToList();

            foreach (ChessMove move in moveList)
            {
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

    public void PerformFakeMove(ChessMove move)
    {
        ChessPiece piece = move.piece;
        ChessPiece targetPiece = move.targetPiece;
        Tile originTile = board.GetTile(move.origin);
        Tile destinationTile = board.GetTile(move.destination);

        originTile.TileState = Tile.State.Free;
        destinationTile.TileState = Tile.State.Occupied;

        piece.position = move.destination;
        if (targetPiece)
        {
            move.targetPiece = targetPiece;
            targetPiece.fakeKilled = true;
        }
        moveStack.Push(move);

        board.pieceList.Remove(move.origin);
        board.pieceList[move.destination] = piece;
    }    

    public void UndoFakeMove()
    {
        ChessMove move = moveStack.Pop();

        ChessPiece piece = move.piece;
        ChessPiece targetPiece = move.targetPiece;
        Tile originTile = board.GetTile(move.origin);
        Tile destinationTile = board.GetTile(move.destination);

        originTile.TileState = Tile.State.Occupied;
        piece.position = move.origin;
        if (targetPiece)
        {
            targetPiece.fakeKilled = false;
            destinationTile.TileState = Tile.State.Occupied;
            board.pieceList[move.destination] = targetPiece;
        }
        else
        {
            destinationTile.TileState = Tile.State.Free;
            board.pieceList.Remove(move.destination);
        }
        board.pieceList[move.origin] = piece;
    }
}
