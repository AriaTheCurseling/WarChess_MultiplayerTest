using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "2D/Tiles/Alternate Rule Tile")]
public class AlternateRuleTile : RuleTile {
    public Sprite m_DefaultSpriteAlt;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
        if (tileData.sprite == m_DefaultSprite && (position.x + position.y + position.z) % 2 == 0)
            tileData.sprite = m_DefaultSpriteAlt;
    }
}