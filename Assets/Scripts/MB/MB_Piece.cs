using UnityEngine;

public class MB_Piece : MonoBehaviour
{
    [SerializeField, NotNull] 
    private SpriteRenderer pieceRenderer;

    [SerializeField, NotNull] 
    private SO_Faction faction;

    [SerializeField, NotNull] 
    private SO_Piece piece;

    private bool hasMoved;

    // Start is called before the first frame update
    void Start()
    {
        UpdateVisuals();
        hasMoved = false;
    }

    void UpdateVisuals()
    {
        pieceRenderer.sprite = piece.Sprite;
        pieceRenderer.color = faction.Color;
    }
}
