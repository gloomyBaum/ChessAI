using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public static GameSetup instance;
    private const float LAYER_HEIGHT_PIECES = 0.00f;
    private const float LAYER_HEIGHT_TILES = -0.1f;
    private const float OVERALL_SCALE = 0.9f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public ChessBoard Setup()
    {
        Dictionary<Vector2Int, Tile> tileList = SetupTiles();
        Dictionary<Vector2Int, ChessPiece> pieceList = SetupPieces();

        return new ChessBoard(tileList, pieceList);
    }
    private Dictionary<Vector2Int, ChessPiece> SetupPieces()
    {
        Dictionary<Vector2Int, ChessPiece> pieceList = new Dictionary<Vector2Int, ChessPiece>();

        //White
        SpawnPiece(0, 0, ChessPiece.Type.Rook, ChessPiece.Color.White, pieceList);
        SpawnPiece(1, 0, ChessPiece.Type.Knight, ChessPiece.Color.White, pieceList);
        SpawnPiece(2, 0, ChessPiece.Type.Bishop, ChessPiece.Color.White, pieceList);
        SpawnPiece(3, 0, ChessPiece.Type.King, ChessPiece.Color.White, pieceList);
        SpawnPiece(4, 0, ChessPiece.Type.Queen, ChessPiece.Color.White, pieceList);
        SpawnPiece(5, 0, ChessPiece.Type.Bishop, ChessPiece.Color.White, pieceList);
        SpawnPiece(6, 0, ChessPiece.Type.Knight, ChessPiece.Color.White, pieceList);
        SpawnPiece(7, 0, ChessPiece.Type.Rook, ChessPiece.Color.White, pieceList);
        for (int i = 0; i < 8; i++)
        {
            SpawnPiece(i, 1, ChessPiece.Type.Pawn, ChessPiece.Color.White, pieceList);
        }
        //Black
        SpawnPiece(0, 7, ChessPiece.Type.Rook, ChessPiece.Color.Black, pieceList);
        SpawnPiece(1, 7, ChessPiece.Type.Knight, ChessPiece.Color.Black, pieceList);
        SpawnPiece(2, 7, ChessPiece.Type.Bishop, ChessPiece.Color.Black, pieceList);
        SpawnPiece(3, 7, ChessPiece.Type.King, ChessPiece.Color.Black, pieceList);
        SpawnPiece(4, 7, ChessPiece.Type.Queen, ChessPiece.Color.Black, pieceList);
        SpawnPiece(5, 7, ChessPiece.Type.Bishop, ChessPiece.Color.Black, pieceList);
        SpawnPiece(6, 7, ChessPiece.Type.Knight, ChessPiece.Color.Black, pieceList);
        SpawnPiece(7, 7, ChessPiece.Type.Rook, ChessPiece.Color.Black, pieceList);
        for (int i = 0; i < 8; i++)
        {
            SpawnPiece(i, 6, ChessPiece.Type.Pawn, ChessPiece.Color.Black, pieceList);
        }
        return pieceList;
    }
    private void SpawnPiece(int x, int y, ChessPiece.Type type, ChessPiece.Color color, Dictionary<Vector2Int, ChessPiece> pieceList)
    {
        
        GameObject go = Instantiate(Resources.Load("ChessGame/Prefabs/" + type.ToString() + color.ToString())) as GameObject;

        //transform Magic
        go.transform.parent = GameObject.Find("ChessBoard").transform;
        go.transform.position = new Vector3(x, LAYER_HEIGHT_PIECES, y);
        go.transform.rotation = Quaternion.Euler(90, 0, 0);
        go.transform.localScale = new Vector3(OVERALL_SCALE, OVERALL_SCALE, OVERALL_SCALE);

        //update position
        go.GetComponent<ChessPiece>().position = new Vector2Int(x, y);

        //set Layer to ignore raycast
        go.layer = LayerMask.NameToLayer("Ignore Raycast");

        //add to list
        pieceList.Add(new Vector2Int(x, y), go.GetComponent<ChessPiece>());

        go.name = type.ToString() + color.ToString();
    }

    private Dictionary<Vector2Int, Tile> SetupTiles()
    {
        Dictionary<Vector2Int, Tile> tileList = new Dictionary<Vector2Int, Tile>();
        //Build Board
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (i % 2 == 0)
                {
                    if (j % 2 == 0)
                    {
                        SpawnTile(i, j, Tile.Color.Black, tileList);
                    }
                    else
                    {
                        SpawnTile(i, j, Tile.Color.White, tileList);
                    }
                }
                else
                {
                    if (j % 2 == 0)
                    {
                        SpawnTile(i, j, Tile.Color.White, tileList);
                    }
                    else
                    {
                        SpawnTile(i, j, Tile.Color.Black, tileList);
                    }
                }
            }
        }
        return tileList;
    }
    private void SpawnTile(int x, int y, Tile.Color color, Dictionary<Vector2Int, Tile> tileList)
    {
        GameObject tile;
        if (color == Tile.Color.Black)
        {
            tile = Instantiate(Resources.Load("ChessGame/Prefabs/TileBlack")) as GameObject;
        }
        else
        {
            tile = Instantiate(Resources.Load("ChessGame/Prefabs/TileWhite")) as GameObject;
        }
        //set position and update position
        tile.transform.position = new Vector3(x, LAYER_HEIGHT_TILES, y);
        tile.GetComponent<Tile>().position = new Vector2Int(x, y);

        //make shiny
        tile.name = "[" + x + "," + y + "]";
        tile.transform.parent = GameObject.Find("ChessBoard").transform;

        //Update Tilestate where pieces are spawning
        if(y < 2 || y > 5)     
            tile.GetComponent<Tile>().TileState = Tile.State.Occupied;
        //add to List
        tileList.Add(new Vector2Int(x, y), tile.GetComponent<Tile>());
    }
}
