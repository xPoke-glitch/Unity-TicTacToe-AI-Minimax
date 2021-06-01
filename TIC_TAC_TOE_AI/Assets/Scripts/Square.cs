using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField] private GameObject marker;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite cross;
    [SerializeField] private Sprite circle;
    [SerializeField] private Board board;
    [SerializeField] private GameManager gameManager;
    public bool IsMarked { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        IsMarked = false;
    }

    // Listener
    public void OnMouseDown() // Human turn
    {
        if (!IsMarked)
        {
            marker.GetComponent<SpriteRenderer>().sprite = circle;
            IsMarked = true;
            board.aviableSquares--;
            gameManager.DidHumanMove = true;
        }
    }

    public void MarkHumanMoveFromAI()
    {
        if (!IsMarked) { 
            marker.GetComponent<SpriteRenderer>().sprite = circle;
            IsMarked = true;
            board.aviableSquares--;
        }
    }

    public void UndoHumanMoveFromAI()
    {
        if (IsMarked)
        {
            marker.GetComponent<SpriteRenderer>().sprite = defaultSprite;
            IsMarked = false;
            board.aviableSquares++;
        }
    }

    public void MarkMoveAI() {
        if (!IsMarked)
        {
            marker.GetComponent<SpriteRenderer>().sprite = cross;
            IsMarked = true;
            board.aviableSquares--;
        }
    }

    public void UndoMoveAI() {
        if (IsMarked)
        {
            marker.GetComponent<SpriteRenderer>().sprite = defaultSprite;
            IsMarked = false;
            board.aviableSquares++;
        }
    }

    public string GetMarkerType() {
        if (marker.GetComponent<SpriteRenderer>().sprite.name.Equals("cross")) {
            return "Cross";
        }
        if (marker.GetComponent<SpriteRenderer>().sprite.name.Equals("circle"))
        {
            return "Circle";
        }
        return "None";
    }

    public void ResetSquare()
    {
        marker.GetComponent<SpriteRenderer>().sprite = defaultSprite;
        IsMarked = false;
    }
}
