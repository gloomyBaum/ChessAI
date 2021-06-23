using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMovement : ChessPieceMovement
{
    public override List<ChessMove> GetMoveList(ChessBoard board)
    {
        moveList = new List<ChessMove>();
        piece = gameObject.GetComponent<ChessPiece>();
        originalX = piece.position.x;
        originalY = piece.position.y;
        currentX = originalX;
        currentY = originalY;

        //white team
        if (piece.chessPieceColor == ChessPiece.Color.White)
        {
            //Dia left 
            currentX = originalX - 1;
            currentY = originalY + 1;
            targetPiece = board.GetChessPiece(new Vector2Int(currentX, currentY));
            if (board.CheckIfCanAttack(piece, targetPiece))
            {
                moveList.Add(new ChessMove(piece, targetPiece, piece.position, targetPiece.position));
            }
            //Dia right 
            currentX = originalX + 1;
            currentY = originalY + 1;
            targetPiece = board.GetChessPiece(new Vector2Int(currentX, currentY));
            if (board.CheckIfCanAttack(piece, targetPiece))
            {
                moveList.Add(new ChessMove(piece, targetPiece, piece.position, targetPiece.position));
            }
            //straight
            currentX = originalX;
            currentY = originalY + 1;
            targetTile = board.GetTile(new Vector2Int(currentX, currentY));
            if (board.CheckTilePassable(targetTile))
            {
                moveList.Add(new ChessMove(piece, piece.position, targetTile.position));
                //double straight
                currentX = originalX;
                currentY = originalY + 2;
                targetTile = board.GetTile(new Vector2Int(currentX, currentY));
                if (board.CheckTilePassable(targetTile) && piece.unmoved)
                {
                    moveList.Add(new ChessMove(piece, piece.position, targetTile.position));
                }
            }
        }
        else
        {
            //Dia left 
            currentX = originalX - 1;
            currentY = originalY - 1;
            targetPiece = board.GetChessPiece(new Vector2Int(currentX, currentY));
            if (board.CheckIfCanAttack(piece, targetPiece))
            {
                moveList.Add(new ChessMove(piece, targetPiece, piece.position, targetPiece.position));
            }
            //Dia right 
            currentX = originalX + 1;
            currentY = originalY - 1;
            targetPiece = board.GetChessPiece(new Vector2Int(currentX, currentY));
            if (board.CheckIfCanAttack(piece, targetPiece))
            {
                moveList.Add(new ChessMove(piece, targetPiece, piece.position, targetPiece.position));
            }
            //straight
            currentX = originalX;
            currentY = originalY - 1;
            targetTile = board.GetTile(new Vector2Int(currentX, currentY));
            if (board.CheckTilePassable(targetTile))
            {
                moveList.Add(new ChessMove(piece, piece.position, targetTile.position));
                //double straight
                currentX = originalX;
                currentY = originalY - 2;
                targetTile = board.GetTile(new Vector2Int(currentX, currentY));
                if (board.CheckTilePassable(targetTile) && piece.unmoved)
                {
                    moveList.Add(new ChessMove(piece, piece.position, targetTile.position));
                }
            }
        }
        return moveList;
    }
}
