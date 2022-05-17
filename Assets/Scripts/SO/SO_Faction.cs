using UnityEngine;

[CreateAssetMenu(menuName ="SO/Faction")]
public class SO_Faction : ScriptableObject
{
    [field: SerializeField] 
    public Color Color { get; private set; } = Color.white;

    [field: SerializeField]
    public Vector2 Forward { get; private set; }

}
