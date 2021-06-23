using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    public Vector2Int position;
    public Type chessPieceType;
    public Color chessPieceColor;
    public bool unmoved;
    private const float LAYER_HEIGHT = 0.00f;

    public int pieceValue;
    public bool fakeKilled;
    public enum Type
    {
        Rook,
        Knight,
        Bishop,
        Queen,
        King,
        Pawn,
    }
    public enum Color
    {
        White,
        Black
    }
    private Vector3 targetVector;
    private Vector3 velocity;
    private void Start()
    {
        targetVector = this.gameObject.transform.position;
        velocity = Vector3.zero;
    }
    private void Update()
    {
        this.gameObject.transform.position = Vector3.SmoothDamp(this.gameObject.transform.position, targetVector, ref velocity, 0.3f);
    }
    public void MovePiece(Vector2Int position)
    {
        targetVector = new Vector3(position.x, LAYER_HEIGHT, position.y);        
    }
    public List<ChessMove> GetPossibleMoves(ChessBoard board)
    {
        return this.gameObject.GetComponent<ChessPieceMovement>().GetMoveList(board);
    }
}
