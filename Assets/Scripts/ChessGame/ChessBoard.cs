using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class ChessBoard
{
    public Dictionary<Vector2Int, Tile> tileList;
    public Dictionary<Vector2Int, ChessPiece> pieceList;
    public ChessBoard(Dictionary<Vector2Int, Tile> tileList, Dictionary<Vector2Int, ChessPiece> pieceList)
    {
        this.tileList = tileList;
        this.pieceList = pieceList;

        highlightedList = new List<Tile>();
    }    
    public void DestroyChessPiece(ChessPiece piece)
    {
        pieceList.Remove(piece.position);
        GameObject.Destroy(piece.gameObject);
    }
    public void MoveChessPiece(ChessPiece piece, Vector2Int position)
    {
        Tile oldTile = tileList[piece.position];
        Tile newTile = tileList[position];
        //move gameobject
        piece.MovePiece(position);
        //update dictionary
        pieceList.Remove(piece.position);
        pieceList.Add(position, piece);
        //update position
        piece.position = position;
        piece.unmoved = false;
        //update Tile state
        newTile.TileState = Tile.State.Occupied;
        oldTile.TileState = Tile.State.Free;
    }

    #region Checkers
    public bool CheckTilePassable(Tile tile)
    {
        if (tile != null && tile.TileState == Tile.State.Free)
        {
            return true;
        }
        return false;
    }
    public bool CheckIfFriendly(ChessPiece cOrigin, ChessPiece cTarget)
    {
        if (cOrigin != null && cTarget != null && cOrigin.chessPieceColor == cTarget.chessPieceColor)
        {
            return true;
        }
        return false;
    }
    public bool CheckIfCanAttack(ChessPiece cOrigin, ChessPiece cTarget)
    {
        if (cOrigin != null && cTarget != null && cOrigin.chessPieceColor != cTarget.chessPieceColor)
        {
            return true;
        }
        return false;
    }
    #endregion

    #region Getters
    public ChessPiece GetChessPiece(Vector2Int position)
    {
        if (pieceList.TryGetValue(position, out ChessPiece piece))
        {
            if (!piece.fakeKilled)
                return piece;
        }
        return null;
    }
    public Tile GetTile(Vector2Int position)
    {
        if (tileList.TryGetValue(position, out Tile tile))
            return tile;
        return null;
    }
    public ChessMove GetValidMove(ChessPiece piece, Vector2Int destination)
    {
        foreach(ChessMove move in piece.GetPossibleMoves(this))
        {
            if (move.piece == piece && move.destination == destination)
            {
                return move;
            }
                           
        }
        return null;
    }
    public List<ChessPiece> GetPieceListOfColor(ChessPiece.Color color)
    {
        List<ChessPiece> result = new List<ChessPiece>();
        foreach (ChessPiece piece in pieceList.Values)
        {
            if (piece.chessPieceColor == color)
                result.Add(piece);
        }
        return result;
    }
    public List<ChessMove> GetAllPossibleMovesOfColor(ChessPiece.Color color)
    {
        List<ChessMove> moveList = new List<ChessMove>();
        foreach(ChessPiece piece in pieceList.Values)
        {
            if(piece.chessPieceColor == color && !piece.fakeKilled)
            {
                foreach (ChessMove move in piece.GetPossibleMoves(this))
                {
                    moveList.Add(move);
                }
            }            
        }
        return moveList;
    }
    #endregion

    #region Tile Highlights
    private List<Tile> highlightedList;

    public void HighlightAttackTiles(ChessPiece piece)
    {
        foreach (ChessMove move in piece.GetPossibleMoves(this))
        {
            Tile tile = tileList[move.destination];
            tile.Highlight(tile.HighlightSprite);
            highlightedList.Add(tile);
        }
    }
    public void CancelHighlights()
    {
        foreach (Tile tile in highlightedList)
        {
            tile.CancelHighlight();
        }
    }
    #endregion
}
