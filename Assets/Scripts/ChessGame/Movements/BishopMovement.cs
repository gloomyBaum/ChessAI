using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopMovement : ChessPieceMovement
{
    public override List<ChessMove> GetMoveList(ChessBoard board)
    {
        moveList = new List<ChessMove>();
        piece = gameObject.GetComponent<ChessPiece>();
        originalX = piece.position.x;
        originalY = piece.position.y;
        currentX = originalX;
        currentY = originalY;

        //Top Left
        currentX = originalX - 1;
        currentY = originalY + 1;
        while (true)
        {
            targetPiece = board.GetChessPiece(new Vector2Int(currentX, currentY));
            targetTile = board.GetTile(new Vector2Int(currentX, currentY));
            if (board.CheckTilePassable(targetTile))
            {
                moveList.Add(new ChessMove(piece, piece.position, targetTile.position));
                currentX--;
                currentY++;
            }
            else if(board.CheckIfCanAttack(piece, targetPiece))
            {
                moveList.Add(new ChessMove(piece, targetPiece, piece.position, targetTile.position));
                break;
            }
            else
            {
                break;
            }
        }
        //Top Right
        currentX = originalX + 1;
        currentY = originalY + 1;
        while (true)
        {
            targetPiece = board.GetChessPiece(new Vector2Int(currentX, currentY));
            targetTile = board.GetTile(new Vector2Int(currentX, currentY));
            if (board.CheckTilePassable(targetTile))
            {
                moveList.Add(new ChessMove(piece, piece.position, targetTile.position));
                currentX++;
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
        //Bottom Left
        currentX = originalX - 1;
        currentY = originalY - 1;
        while (true)
        {
            targetPiece = board.GetChessPiece(new Vector2Int(currentX, currentY));
            targetTile = board.GetTile(new Vector2Int(currentX, currentY));
            if (board.CheckTilePassable(targetTile))
            {
                moveList.Add(new ChessMove(piece, piece.position, targetTile.position));
                currentX--;
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
        //Bottom Right
        currentX = originalX + 1;
        currentY = originalY - 1;
        while (true)
        {
            targetPiece = board.GetChessPiece(new Vector2Int(currentX, currentY));
            targetTile = board.GetTile(new Vector2Int(currentX, currentY));
            if (board.CheckTilePassable(targetTile))
            {
                moveList.Add(new ChessMove(piece, piece.position, targetTile.position));
                currentX++;
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
