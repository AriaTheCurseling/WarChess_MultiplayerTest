using UnityEngine;

[CreateAssetMenu(menuName = "SO/Move")]
public class SO_Move : ScriptableObject
{
    [field: SerializeField, Optional] public Vector2Int dir   { get; private set; }
    [field: SerializeField] public bool CanKill     { get; private set; } = true;
    [field: SerializeField] public bool CanMove     { get; private set; } = true;
    [field: SerializeField] public bool Repeating   { get; private set; } = true;
    [field: SerializeField] public bool Flip        { get; private set; } = true;
    [field: SerializeField] public bool Rotate      { get; private set; } = true;
}
