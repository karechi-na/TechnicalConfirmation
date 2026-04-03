using UnityEngine;

/// <summary>
/// 箱オブジェクトのクラス
/// </summary>
public class BoxTile : MonoBehaviour
{
    [Header("References")]

    [Header("Positioning")]
    [SerializeField] private float yOffset = 0.35f;

    [Header("Grid Settings")]
    [SerializeField] private Vector2Int startGridPosition = Vector2Int.zero;

    // 現在のグリッド上の位置
    public Vector2Int CurrentGridPosition { get; private set; }

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        Initialize(startGridPosition);
    }

    /// <summary>
    /// 初期化メソッド
    /// </summary>
    public void Initialize(Vector2Int gridPosition)
    {
        PositionSet(gridPosition);
    }

    /// <summary>
    /// 移動メソッド
    /// </summary>
    public void MoveTo(Vector2Int targetGridPosition)
    {
        PositionSet(targetGridPosition);
    }

    /// <summary>
    /// 位置更新メソッド
    /// </summary>
    private void PositionSet(Vector2Int gridPosition)
    {
        // グリッド上の位置を更新し、ワールド座標に変換して配置
        CurrentGridPosition = gridPosition;
        transform.position = GridManager.Instance.GridToWorldPosition(CurrentGridPosition, yOffset);
    }
}
