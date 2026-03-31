using UnityEngine;

public class FloorTile : GridTileBase
{
    public override bool CanEnter(PlayerType playerType)
    {
        return true;
    }

    public override void OnEnter(PlayerController player)
    {

    }
}
