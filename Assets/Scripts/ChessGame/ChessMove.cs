using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessMove
{
    public Vector2Int origin;
    public Vector2Int destination;
    public ChessPiece piece;
    public ChessPiece targetPiece;

    public ChessMove(ChessPiece piece, Vector2Int origin, Vector2Int destination)
    {
        this.piece = piece;
        this.origin = origin;
        this.destination = destination;
    }
    public ChessMove(ChessPiece piece, ChessPiece targetPiece, Vector2Int origin, Vector2Int destination)
    {
        this.piece = piece;
        this.targetPiece = targetPiece;
        this.origin = origin;
        this.destination = destination;
    }

}
