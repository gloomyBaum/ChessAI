using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovement : ChessPieceMovement
{
    public override List<ChessMove> GetMoveList(ChessBoard board)
    {
        moveList = new List<ChessMove>();
        piece = gameObject.GetComponent<ChessPiece>();
        originalX = piece.position.x;
        originalY = piece.position.y;
        currentX = originalX;
        currentY = originalY;

        //top left 1
        currentX = originalX - 1;
        currentY = originalY + 2;
        targetPiece = board.GetChessPiece(new Vector2Int(currentX, currentY));
        targetTile = board.GetTile(new Vector2Int(currentX, currentY));
        if (board.CheckTilePassable(targetTile))
        {
            moveList.Add(new ChessMove(piece, piece.position, targetTile.position));
        }
        else if (board.CheckIfCanAttack(piece, targetPiece))
        {
            moveList.Add(new ChessMove(piece, targetPiece, piece.position, targetTile.position));
        }
        //top left 2
        currentX = originalX - 2;
        currentY = originalY + 1;
        targetPiece = board.GetChessPiece(new Vector2Int(currentX, currentY));
        targetTile = board.GetTile(new Vector2Int(currentX, currentY));
        if (board.CheckTilePassable(targetTile))
        {
            moveList.Add(new ChessMove(piece, piece.position, targetTile.position));
        }
        else if (board.CheckIfCanAttack(piece, targetPiece))
        {
            moveList.Add(new ChessMove(piece, targetPiece, piece.position, targetTile.position));
        }
        //bot left 1 
        currentX = originalX - 1;
        currentY = originalY - 2;
        targetPiece = board.GetChessPiece(new Vector2Int(currentX, currentY));
        targetTile = board.GetTile(new Vector2Int(currentX, currentY));
        if (board.CheckTilePassable(targetTile))
        {
            moveList.Add(new ChessMove(piece, piece.position, targetTile.position));
        }
        else if (board.CheckIfCanAttack(piece, targetPiece))
        {
            moveList.Add(new ChessMove(piece, targetPiece, piece.position, targetTile.position));
        }
        //bot left 2
        currentX = originalX - 2;
        currentY = originalY - 1;
        targetPiece = board.GetChessPiece(new Vector2Int(currentX, currentY));
        targetTile = board.GetTile(new Vector2Int(currentX, currentY));
        if (board.CheckTilePassable(targetTile))
        {
            moveList.Add(new ChessMove(piece, piece.position, targetTile.position));
        }
        else if (board.CheckIfCanAttack(piece, targetPiece))
        {
            moveList.Add(new ChessMove(piece, targetPiece, piece.position, targetTile.position));
        }
        //top right 1
        currentX = originalX + 1;
        currentY = originalY + 2;
        targetPiece = board.GetChessPiece(new Vector2Int(currentX, currentY));
        targetTile = board.GetTile(new Vector2Int(currentX, currentY));
        if (board.CheckTilePassable(targetTile))
        {
            moveList.Add(new ChessMove(piece, piece.position, targetTile.position));
        }
        else if (board.CheckIfCanAttack(piece, targetPiece))
        {
            moveList.Add(new ChessMove(piece, targetPiece, piece.position, targetTile.position));
        }
        //top right 2
        currentX = originalX + 2;
        currentY = originalY + 1;
        targetPiece = board.GetChessPiece(new Vector2Int(currentX, currentY));
        targetTile = board.GetTile(new Vector2Int(currentX, currentY));
        if (board.CheckTilePassable(targetTile))
        {
            moveList.Add(new ChessMove(piece, piece.position, targetTile.position));
        }
        else if (board.CheckIfCanAttack(piece, targetPiece))
        {
            moveList.Add(new ChessMove(piece, targetPiece, piece.position, targetTile.position));
        }
        //bot right 1 
        currentX = originalX + 1;
        currentY = originalY - 2;
        targetPiece = board.GetChessPiece(new Vector2Int(currentX, currentY));
        targetTile = board.GetTile(new Vector2Int(currentX, currentY));
        if (board.CheckTilePassable(targetTile))
        {
            moveList.Add(new ChessMove(piece, piece.position, targetTile.position));
        }
        else if (board.CheckIfCanAttack(piece, targetPiece))
        {
            moveList.Add(new ChessMove(piece, targetPiece, piece.position, targetTile.position));
        }
        //bot right 2 
        currentX = originalX + 2;
        currentY = originalY - 1;
        targetPiece = board.GetChessPiece(new Vector2Int(currentX, currentY));
        targetTile = board.GetTile(new Vector2Int(currentX, currentY));
        if (board.CheckTilePassable(targetTile))
        {
            moveList.Add(new ChessMove(piece, piece.position, targetTile.position));
        }
        else if (board.CheckIfCanAttack(piece, targetPiece))
        {
            moveList.Add(new ChessMove(piece, targetPiece, piece.position, targetTile.position));
        }
        return moveList;
    }
}

