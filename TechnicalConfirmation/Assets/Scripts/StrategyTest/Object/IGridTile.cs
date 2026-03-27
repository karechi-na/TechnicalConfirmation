using UnityEngine;

public interface IGridTile
{
    bool CanEnter(PlayerType playerType);
    void OnEnter(PlayerController player);
}
