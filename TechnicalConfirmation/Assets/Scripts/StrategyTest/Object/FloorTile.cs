/// <summary>
/// ただの床タイル
/// </summary>
public class FloorTile : GridTileBase
{
    /// <summary>
    /// 全部の状態のプレイヤーが入れるようにする
    /// </summary>
    public override bool CanEnter(PlayerType playerType)
    {
        return true;
    }

    /// <summary>
    /// タイルに入ったときの処理
    /// FloorTileは特に何もないので空のまま
    /// </summary>
    public override void OnEnter(PlayerController player)
    {

    }
}
