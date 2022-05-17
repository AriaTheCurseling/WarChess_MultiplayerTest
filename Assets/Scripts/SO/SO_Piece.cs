using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "SO/Piece")]
public class SO_Piece : ScriptableObject
{
    [field: SerializeField, NotNull] 
    public Sprite Sprite { get; private set; }

    [field: SerializeField] 
    public SO_Move[] Moves { get; private set; }

}