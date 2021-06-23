using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieceMovement : MonoBehaviour
{
    protected List<ChessMove> moveList;
    protected ChessPiece piece;
    protected ChessPiece targetPiece;
    protected Tile targetTile;
    protected int originalX;
    protected int originalY;
    protected int currentX;
    protected int currentY;
    public virtual List<ChessMove> GetMoveList(ChessBoard board)
    {
        List<ChessMove> list = new List<ChessMove>();
        return list;
    }
}
