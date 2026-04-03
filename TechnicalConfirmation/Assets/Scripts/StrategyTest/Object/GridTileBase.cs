using UnityEngine;

/// <summary>
/// 各Tileクラスの抽象規定クラス
/// </summary>
public abstract class GridTileBase : MonoBehaviour, IGridTile
{
    public abstract bool CanEnter(PlayerType playerType);
    public abstract void OnEnter(PlayerController player);
}
