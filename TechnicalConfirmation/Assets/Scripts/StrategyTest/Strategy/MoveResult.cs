using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移動の結果を表すクラス
/// </summary>
public class MoveResult
{
    // 移動可能かどうか
    public bool CanMove { get; private set; }
    // プレイヤーの移動先の座標
    public Vector2Int TargetPosition { get; private set; }
    // プレイヤーが移動するルートの座標リスト
    public List<Vector2Int> Route { get; private set; }
    // プレイヤーが移動するのにかかる時間
    public float MoveDuration { get; private set; }

    // プレイヤーが箱を動かす移動かどうか
    public bool HasBoxMove { get; private set; }
    // プレイヤーが動かす箱の現在位置
    public Vector2Int BoxCurrentPosition { get; private set; }
    // プレイヤーが動かす箱の移動先の座標
    public Vector2Int BoxTargetPosition { get; private set; }

    // コンストラクタ
    public MoveResult(
        bool canMove, 
        Vector2Int targetPosition, 
        List<Vector2Int> route,
        float moveDuration,
        bool hasBoxMove = false,
        Vector2Int boxCurrentPosition = default,
        Vector2Int boxTargetPosition = default)
    {
        CanMove = canMove;
        TargetPosition = targetPosition;
        Route = route;
        MoveDuration = moveDuration;
        HasBoxMove = hasBoxMove;
        BoxCurrentPosition = boxCurrentPosition;
        BoxTargetPosition = boxTargetPosition;
    }

    /// <summary>
    /// 移動失敗の結果を返す静的メソッド
    /// </summary>
    public static MoveResult Fail(Vector2Int currentPosition)
    {
        return new MoveResult(
            false,
            currentPosition,
            null,
            0.0f
            );
    }
}
