using UnityEngine;

public class FloorTile : MonoBehaviour, IGridTile
{
    public bool CanEnter(PlayerType playerType)
    {
        return true;
    }

    public void OnEnter(PlayerController player)
    {

    }
}
