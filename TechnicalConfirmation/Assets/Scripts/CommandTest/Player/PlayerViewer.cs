using UnityEngine;

/// <summary>
/// Playerの位置を更新するクラス
/// </summary>
public class PlayerViewer : MonoBehaviour
{
    [Header("Playerの座標情報")]
    [SerializeField] private Transform nowPosition = null;

    /// <summary>
    /// 初期化メソッド
    /// </summary>
    public void Initialized(Simulate simulate)
    {
        simulate.OnPositionChanged += Position;
    }

    /// <summary>
    /// Simulateクラスから座標が変更されたときに呼び出されるメソッド
    /// </summary>
    private void Position(Vector2 vector)
    {
        // 実際のPlayerの座標を更新
        nowPosition.position = new Vector3(
            vector.x,
            nowPosition.position.y,
            vector.y
            );
    }
}
