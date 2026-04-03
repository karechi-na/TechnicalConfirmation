using System;

/// <summary>
/// Heavyタイプのプレイヤーが乗ると状態が切り替わるスイッチタイル
/// </summary>
public class HeavySwitchTile : GridTileBase
{
    // スイッチの状態が切り替わったときに発火するイベント
    public event Action<bool> OnHeavySwitchActivated;
    // スイッチの状態を保持するフラグ
    private bool isActivated = false;

    /// <summary>
    /// プレイヤーがこのタイルに入ることができるかどうかを判断するメソッド
    /// 全部のプレイヤータイプが入れるようにする（Heavy以外も入れることで、スイッチの状態を確認できるようにする）
    /// </summary>
    public override bool CanEnter(PlayerType playerType)
    {
        return true;
    }

    /// <summary>
    /// Heavyタイプのプレイヤーがこのタイルに入ると、スイッチの状態が切り替わるメソッド
    /// </summary>
    public override void OnEnter(PlayerController player)
    {
        if (player.CurrentType != PlayerType.Heavy) return;

        // スイッチの状態を切り替える
        isActivated = !isActivated;
        // スイッチの状態が切り替わったことを通知するイベントを発火させる
        OnHeavySwitchActivated?.Invoke(isActivated);
    }
}
