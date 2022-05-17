using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameMode")]
public class SO_GameMode : ScriptableObject
{
    [field: SerializeField]
    public SerializableDictionary<Vector2,T<SO_Faction,SO_Piece>> Pieces { get; private set; }
}
