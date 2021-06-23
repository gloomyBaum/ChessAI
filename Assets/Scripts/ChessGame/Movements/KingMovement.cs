using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingMovement : ChessPieceMovement
{
    public override List<ChessMove> GetMoveList(ChessBoard board)
    {
        moveList = new List<ChessMove>();
        piece = gameObject.GetComponent<ChessPiece>();
        originalX = piece.position.x;
        originalY = piece.position.y;
        currentX = originalX;
        currentY = originalY;

        //top left
        currentX = originalX - 1;
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
        //left left
        currentX = originalX - 1;
        currentY = originalY;
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
        //bottom left
        currentX = originalX - 1;
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
        //top right
        currentX = originalX + 1;
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
        //right right
        currentX = originalX + 1;
        currentY = originalY;
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
        //bottom right
        currentX = originalX + 1;
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
        //top top
        currentX = originalX;
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
        //bottom bottom
        currentX = originalX;
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
