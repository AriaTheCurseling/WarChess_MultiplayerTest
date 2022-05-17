using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(menuName = "2D/Tiles/Interaction Rule Tile")]
public class InteractionRuleTile : RuleTile<InteractionRuleTile.Neighbor> {
    public TileBase otherTile;

    public class Neighbor : RuleTile.TilingRule.Neighbor {
        public const int Other = 3;
    }

    public override bool RuleMatch(int neighbor, TileBase tile) {
        switch (neighbor) {
            case Neighbor.Other: return tile?.GetType() == otherTile?.GetType();
        }
        return base.RuleMatch(neighbor, tile);
    }
}