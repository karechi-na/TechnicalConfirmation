using UnityEngine;

public class FenceTile : GridTileBase
{
    public override bool CanEnter(PlayerType playerType)
    {
        return false;
    }

    public override void OnEnter(PlayerController player)
    {

    }
}
