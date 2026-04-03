using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NormalPlayerの移動方法を定義するクラス
/// </summary>
public class NormalMoveStrategy : IMoveStrategy
{
    private const float MOVE_DURATION = 0.15f;

    /// <summary>
    /// MoveResultを返す。移動できない場合はMoveResult.CanMoveがfalseになる。
    /// </summary>
    public MoveResult TryMove(
        Vector2Int currentPosition,
        Vector2Int moveDirection,
        GridManager gridManager
        )
    {
        // 移動先の位置を計算
        Vector2Int nextPosition = currentPosition + moveDirection;

        // 移動先にBoxが存在するか確認
        if (gridManager.HasBox(nextPosition))
        {
            return MoveResult.Fail(currentPosition);
        }

        // 移動先に入れるか確認
        if (!gridManager.CanEnter(nextPosition, PlayerType.Normal))
        {
            return MoveResult.Fail(currentPosition);
        }

        // 移動先に入れる場合は、移動先の位置とルートをMoveResultに設定して返す
        List<Vector2Int> route = new List<Vector2Int>
        {
            nextPosition
        };

        return new MoveResult(
            true, 
            nextPosition, 
            route, 
            MOVE_DURATION
            );
    }
}
