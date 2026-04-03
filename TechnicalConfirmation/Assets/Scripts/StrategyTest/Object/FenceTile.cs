/// <summary>
/// フェンスタイルのタイルクラス
/// </summary>
public class FenceTile : GridTileBase
{
    /// <summary>
    /// 基本的にどのプレイヤーも入れないようにする
    /// GhostStrategy側でフェンスか判定を取って通過できるようにしている
    /// </summary>
    public override bool CanEnter(PlayerType playerType)
    {
        return false;
    }

    /// <summary>
    /// 特に何もすることはないが、フェンスを通過できるようにするためにオーバーライドしている
    /// </summary>
    public override void OnEnter(PlayerController player)
    {

    }
}
