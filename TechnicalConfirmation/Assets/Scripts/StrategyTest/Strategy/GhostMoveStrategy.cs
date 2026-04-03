using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GhostPlayerの移動定義クラス
/// </summary>
public class GhostMoveStrategy : IMoveStrategy
{
    private const float MOVE_DURATION = 0.35f;

    /// <summary>
    /// MoveResultを返す。フェンスはすり抜けることができるが、箱は押せないため、箱がある場合は移動できない。
    /// </summary>
    public MoveResult TryMove(
        Vector2Int currentPosition,
        Vector2Int moveDirection,
        GridManager gridManager
        )
    {
        // 移動先の位置を計算
        Vector2Int checkPosition = currentPosition + moveDirection;
        // 移動ルートを格納するリスト
        List<Vector2Int> route = new List<Vector2Int>();

        while (true)
        {
            // 移動先のタイルが存在しない場合は移動失敗
            if (!gridManager.ExistsTile(checkPosition))
            {
                return MoveResult.Fail(currentPosition);
            }

            // フェンスがある場合はすり抜けることができるため、ルートに追加して次の位置をチェック
            if (gridManager.IsFence(checkPosition))
            {
                route.Add(checkPosition);
                checkPosition += moveDirection; 
                continue;
            }

            // 箱がある場合は移動できないため、移動失敗
            if (gridManager.HasBox(checkPosition))
            {
                return MoveResult.Fail(currentPosition);
            }

            // 移動先のタイルが存在し、フェンスも箱もない場合は移動成功
            if (!gridManager.CanEnter(checkPosition, PlayerType.Ghost))
            {
                return MoveResult.Fail(currentPosition);
            }

            // 移動ルートに最終的な移動先を追加
            route.Add(checkPosition);

            // 移動成功の結果を返す
            return new MoveResult(
                true,
                checkPosition,
                route,
                MOVE_DURATION
                );
        }
    }
}
