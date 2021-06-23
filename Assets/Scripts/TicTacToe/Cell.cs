using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Sprite originalSprite;
    public Sprite xSprite;
    public Sprite oSprite;

    public Vector2Int pos;
    public State fieldState;
    public enum State
    {
        Free,
        O,
        X
    }
    private void Start()
    {
        fieldState = State.Free;
        pos = new Vector2Int((int)transform.position.x, (int)transform.position.y);
    }
    private void OnMouseDown()
    {
        TSessionManager.instance.HandleClick(this);
    }
    public void SetX()
    {
        GetComponent<SpriteRenderer>().sprite = xSprite;
        fieldState = State.X;
    }
    public void SetO()
    {
        GetComponent<SpriteRenderer>().sprite = oSprite;
        fieldState = State.O;
    }
}
