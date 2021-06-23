using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HardAI : ChessAICore
{
    #region pieceWeights
    int[,] pawnBlackWeight = new int[,]
    {
        {0,  0,  0,  0,  0,  0,  0,  0},
        {5, 10, 10,-20,-20, 10, 10,  5},
        {5, -5,-10,  0,  0,-10, -5,  5},
        {0,  0,  0, 20, 20,  0,  0,  0},
        {5,  5, 10, 25, 25, 10,  5,  5},
        {10, 10, 20, 30, 30, 20, 10, 10},
        {50, 50, 50, 50, 50, 50, 50, 50},
        {0,  0,  0,  0,  0,  0,  0,  0}
    };
    int[,] pawnWhiteWeight = new int[,]
     {
        {0,  0,  0,  0,  0,  0,  0,  0},
        {50, 50, 50, 50, 50, 50, 50, 50},
        {10, 10, 20, 30, 30, 20, 10, 10},
        {5,  5, 10, 25, 25, 10,  5,  5},
        {0,  0,  0, 20, 20,  0,  0,  0},
        {5, -5,-10,  0,  0,-10, -5,  5},
        {5, 10, 10,-20,-20, 10, 10,  5},
        {0,  0,  0,  0,  0,  0,  0,  0}
     };
    int[,] knightWhiteWeight = new int[,]
    {
        {-50,-40,-30,-30,-30,-30,-40,-50},
        {-40,-20,  0,  0,  0,  0,-20,-40},
        {-30,  0, 10, 15, 15, 10,  0,-30},
        {-30,  5, 15, 20, 20, 15,  5,-30},
        {-30,  0, 15, 20, 20, 15,  0,-30},
        {-30,  5, 10, 15, 15, 10,  5,-30},
        {-40,-20,  0,  5,  5,  0,-20,-40},
        {-50,-40,-30,-30,-30,-30,-40,-50}
    };

    int[,] knightBlackWeight = new int[,]
    {
        {-50,-40,-30,-30,-30,-30,-40,-50},
        {-40,-20,  0,  5,  5,  0,-20,-40},
        {-30,  5, 10, 15, 15, 10,  5,-30},
        {-30,  0, 15, 20, 20, 15,  0,-30},
        {-30,  5, 15, 20, 20, 15,  5,-30},
        {-30,  0, 10, 15, 15, 10,  0,-30},
        {-40,-20,  0,  0,  0,  0,-20,-40},
        {-50,-40,-30,-30,-30,-30,-40,-50}
    };

    int[,] bishopWhiteWeight = new int[,]
    {
        {-20,-10,-10,-10,-10,-10,-10,-20},
        {-10,  0,  0,  0,  0,  0,  0,-10},
        {-10,  0,  5, 10, 10,  5,  0,-10},
        {-10,  5,  5, 10, 10,  5,  5,-10},
        {-10,  0, 10, 10, 10, 10,  0,-10},
        {-10, 10, 10, 10, 10, 10, 10,-10},
        {-10,  5,  0,  0,  0,  0,  5,-10},
        {-20,-10,-10,-10,-10,-10,-10,-20}
    };

    int[,] bishopBlackWeight = new int[,]
    {
        {-20,-10,-10,-10,-10,-10,-10,-20},
        {-10,  5,  0,  0,  0,  0,  5,-10},
        {-10, 10, 10, 10, 10, 10, 10,-10},
        {-10,  0, 10, 10, 10, 10,  0,-10},
        {-10,  5,  5, 10, 10,  5,  5,-10},
        {-10,  0,  5, 10, 10,  5,  0,-10},
        {-10,  0,  0,  0,  0,  0,  0,-10},
        {-20,-10,-10,-10,-10,-10,-10,-20}
    };

    int[,] rookWhiteWeight = new int[,]
    {
        {0,  0,  0,  0,  0,  0,  0,  0},
        {5, 10, 10, 10, 10, 10, 10,  5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {0,  0,  0,  5,  5,  0,  0,  0}
    };

    int[,] rookBlackWeight = new int[,]
    {
        {0,  0,  0,  5,  5,  0,  0,  0},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {5, 10, 10, 10, 10, 10, 10,  5},
        {0,  0,  0,  0,  0,  0,  0,  0}
    };

    int[,] queenWhiteWeight = new int[,]
    {
        {-20,-10,-10, -5, -5,-10,-10,-20},
        {-10,  0,  0,  0,  0,  0,  0,-10},
        {-10,  0,  5,  5,  5,  5,  0,-10},
        {-5,  0,  5,  5,  5,  5,  0, -5},
        {0,  0,  5,  5,  5,  5,  0, -5},
        {-10,  5,  5,  5,  5,  5,  0,-10},
        {-10,  0,  5,  0,  0,  0,  0,-10},
        {-20,-10,-10, -5, -5,-10,-10,-20}
    };

    int[,] queenBlackWeight = new int[,]
    {
        {-20,-10,-10, -5, -5,-10,-10,-20},
        {-10,  0,  5,  0,  0,  0,  0,-10},
        {-10,  5,  5,  5,  5,  5,  0,-10},
        {0,  0,  5,  5,  5,  5,  0, -5},
        {-5,  0,  5,  5,  5,  5,  0, -5},
        {-10,  0,  5,  5,  5,  5,  0,-10},
        {-10,  0,  0,  0,  0,  0,  0,-10},
        {-20,-10,-10, -5, -5,-10,-10,-20}
    };

    int[,] kingWhiteWeight =
    {
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-20,-30,-30,-40,-40,-30,-30,-20},
        {-10,-20,-20,-20,-20,-20,-20,-10},
        {20, 20,  0,  0,  0,  0, 20, 20},
        {20, 30, 10,  0,  0, 10, 30, 20}
    };

    int[,] kingBlackWeight =
    {
        {20, 30, 10,  0,  0, 10, 30, 20},
        {20, 20,  0,  0,  0,  0, 20, 20},
        {-10,-20,-20,-20,-20,-20,-20,-10},
        {-20,-30,-30,-40,-40,-30,-30,-20},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
    };
    #endregion
    public HardAI(ChessBoard board, int maxCalcDepth = 3)
    {
        this.board = board;
        SetPieceValues(board.pieceList);
        moveStack = new Stack<ChessMove>();
        // set how many moves the AI should calculate ahead
        this.maxCalcDepth = maxCalcDepth;
    }
    /// <summary>
    /// Generates Move that consider Value, Positions and future Moves of the enemy
    /// </summary>
    /// <returns></returns>
    public override ChessMove NewAIMove()
    {
        Minimax(maxCalcDepth, -10000000, 10000000, true);
        return bestMove;
    }
    
    /// <summary>
    /// Getter for the specific weight of the piece
    /// </summary>
    /// <param name="piece"></param>
    /// <param name="color"></param>
    /// <param name="pos"></param>
    /// <returns>Value of ChessPiece</returns>
    private int GetPieceWeight(ChessPiece piece, ChessPiece.Color color, Vector2Int pos)
    {
        if(color == ChessPiece.Color.White)
        {
            switch (piece.chessPieceType)
            {
                case ChessPiece.Type.Rook:
                    return rookWhiteWeight[pos.x, pos.y];
                case ChessPiece.Type.Knight:
                    return knightWhiteWeight[pos.x, pos.y];
                case ChessPiece.Type.Bishop:
                    return bishopWhiteWeight[pos.x, pos.y];
                case ChessPiece.Type.Queen:
                    return queenWhiteWeight[pos.x, pos.y];
                case ChessPiece.Type.King:
                    return kingWhiteWeight[pos.x, pos.y];
                case ChessPiece.Type.Pawn:
                    return pawnWhiteWeight[pos.x, pos.y];
                default:
                    return 0;
            }
        }
        else
        {
            switch (piece.chessPieceType)
            {
                case ChessPiece.Type.Rook:
                    return rookBlackWeight[pos.x, pos.y];
                case ChessPiece.Type.Knight:
                    return knightBlackWeight[pos.x, pos.y];
                case ChessPiece.Type.Bishop:
                    return bishopBlackWeight[pos.x, pos.y];
                case ChessPiece.Type.Queen:
                    return queenBlackWeight[pos.x, pos.y];
                case ChessPiece.Type.King:
                    return kingBlackWeight[pos.x, pos.y];
                case ChessPiece.Type.Pawn:
                    return pawnBlackWeight[pos.x, pos.y];
                default:
                    return 0;
            }
        }
        
    }

    /// <summary>
    /// Calculates Board State, includes Piece Positions
    /// </summary>
    /// <returns>Sum of all Piece Values currently on the Board</returns>
    public override int EvalBoard()
    {
        int value = 0;
        foreach (ChessPiece piece in board.pieceList.Values)
        {
            if (!piece.fakeKilled)
            {
                if (piece.chessPieceColor == ChessPiece.Color.Black)
                {
                    value += (piece.pieceValue + GetPieceWeight(piece, ChessPiece.Color.Black, piece.position));
                }
                else
                {
                    value += ((piece.pieceValue + GetPieceWeight(piece, ChessPiece.Color.White, piece.position)) * -1);
                }
            }
        }
        return value;
    }
}
