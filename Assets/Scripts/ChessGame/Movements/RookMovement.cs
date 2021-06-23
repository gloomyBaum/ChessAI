using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookMovement : ChessPieceMovement
{
    public override List<ChessMove> GetMoveList(ChessBoard board)
    {
        moveList = new List<ChessMove>();
        piece = gameObject.GetComponent<ChessPiece>();
        originalX = piece.position.x;
        originalY = piece.position.y;
        currentX = originalX;
        currentY = originalY;

        //left
        currentX = originalX - 1;
        currentY = originalY;
        while (true)
        {
            targetPiece = board.GetChessPiece(new Vector2Int(currentX, currentY));
            targetTile = board.GetTile(new Vector2Int(currentX, currentY));
            if (board.CheckTilePassable(targetTile))
            {
                moveList.Add(new ChessMove(piece, piece.position, targetTile.position));
                currentX--;
            }
            else if (board.CheckIfCanAttack(piece, targetPiece))
            {
                moveList.Add(new ChessMove(piece, targetPiece, piece.position, targetTile.position));
                break;
            }
            else
            {
                break;
            }
        }
        //right
        currentX = originalX + 1;
        currentY = originalY;
        while (true)
        {
            targetPiece = board.GetChessPiece(new Vector2Int(currentX, currentY));
            targetTile = board.GetTile(new Vector2Int(currentX, currentY));
            if (board.CheckTilePassable(targetTile))
            {
                moveList.Add(new ChessMove(piece, piece.position, targetTile.position));
                currentX++;
            }
            else if (board.CheckIfCanAttack(piece, targetPiece))
            {
                moveList.Add(new ChessMove(piece, targetPiece, piece.position, targetTile.position));
                break;
            }
            else
            {
                break;
            }
        }
        //top
        currentX = originalX;
        currentY = originalY + 1;
        while (true)
        {
            targetPiece = board.GetChessPiece(new Vector2Int(currentX, currentY));
            targetTile = board.GetTile(new Vector2Int(currentX, currentY));
            if (board.CheckTilePassable(targetTile))
            {
                moveList.Add(new ChessMove(piece, piece.position, targetTile.position));
                currentY++;
            }
            else if (board.CheckIfCanAttack(piece, targetPiece))
            {
                moveList.Add(new ChessMove(piece, targetPiece, piece.position, targetTile.position));
                break;
            }
            else
            {
                break;
            }
        }
        //Bottom
        currentX = originalX;
        currentY = originalY - 1;
        while (true)
        {
            targetPiece = board.GetChessPiece(new Vector2Int(currentX, currentY));
            targetTile = board.GetTile(new Vector2Int(currentX, currentY));
            if (board.CheckTilePassable(targetTile))
            {
                moveList.Add(new ChessMove(piece, piece.position, targetTile.position));
                currentY--;
            }
            else if (board.CheckIfCanAttack(piece, targetPiece))
            {
                moveList.Add(new ChessMove(piece, targetPiece, piece.position, targetTile.position));
                break;
            }
            else
            {
                break;
            }
        }

        return moveList;
    }
}
