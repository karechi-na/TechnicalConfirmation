using UnityEngine;

public abstract class GridTileBase : MonoBehaviour, IGridTile
{
    public abstract bool CanEnter(PlayerType playerType);
    public abstract void OnEnter(PlayerController player);
}
