using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Heavyタイプのプレイヤーの移動戦略を定義するクラス
/// </summary>
public class HeavyMoveStrategy: IMoveStrategy
{
    private const float MOVE_DURATION = 0.22f;

    /// <summary>
    /// MoveResultを返す。Heavyタイプのプレイヤーは、通常の移動に加えて、箱を押すことができる。
    /// </summary>
    public MoveResult TryMove(
        Vector2Int currentPosition,
        Vector2Int moveDirection,
        GridManager gridManager
        )
    {
        // プレイヤーの次の位置を計算
        Vector2Int nextPosition = currentPosition + moveDirection;

        // 次の位置に箱があるか確認
        if (gridManager.HasBox(nextPosition))
        {
            // 箱を押すためのターゲット位置を計算
            Vector2Int boxTargetPosition = nextPosition + moveDirection;

            // 箱を押すことができるか確認
            if (!gridManager.CanBoxMoveTo(boxTargetPosition))
            {
                return MoveResult.Fail(currentPosition);
            }

            // プレイヤーが次の位置に移動できるか確認
            List<Vector2Int> route = new List<Vector2Int>
            {
                nextPosition
            };

            //移動と箱を押すことができる場合、MoveResultを返す
            return new MoveResult(
                true,
                nextPosition,
                route,
                MOVE_DURATION,
                true,
                nextPosition,
                boxTargetPosition);
        }

        // プレイヤーが次の位置に移動できるか確認
        if (!gridManager.CanEnter(nextPosition, PlayerType.Heavy))
        {
            return MoveResult.Fail(currentPosition);
        }

        // 通常の移動ができる場合、MoveResultを返す
        List<Vector2Int> normalRoute = new List<Vector2Int>()
        {
            nextPosition
        };

        return new MoveResult(
            true,
            nextPosition,
            normalRoute,
            MOVE_DURATION
            );
    }
}
