using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//to prevent mouseclicks going trough pause menu
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    public Vector2Int position;

    public State TileState;

    [SerializeField]
    private Sprite OriginalSprite;
    public Sprite CurrentSprite;
    public Sprite MouseOverSprite;
    public Sprite MouseOverHighlightSprite;
    public Sprite HighlightSprite;

    public Color color;
    public enum Color
    {
        White,
        Black
    }
    public enum State
    {
        Free,
        Occupied
    }
    private void Start()
    {
        CurrentSprite = OriginalSprite;
    }
    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (CurrentSprite == HighlightSprite)
                gameObject.GetComponent<SpriteRenderer>().sprite = MouseOverHighlightSprite;
            else
                gameObject.GetComponent<SpriteRenderer>().sprite = MouseOverSprite;
        }

    }
    private void OnMouseExit()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            gameObject.GetComponent<SpriteRenderer>().sprite = CurrentSprite;
    }
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            SessionManager.instance.HandleMouseClick(position);
    }
    public void Highlight(Sprite sprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        CurrentSprite = sprite;
    }
    public void CancelHighlight()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = OriginalSprite;
        CurrentSprite = OriginalSprite;
    }
    public void CancelMouseOverHighlight()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = CurrentSprite;
    }
}
