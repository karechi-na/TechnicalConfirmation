using UnityEngine;

public class FenceTile : MonoBehaviour,IGridTile
{
    public bool CanEnter(PlayerType playerType)
    {
        return playerType == PlayerType.Ghost;
    }

    public void OnEnter(PlayerController player)
    {

    }
}
