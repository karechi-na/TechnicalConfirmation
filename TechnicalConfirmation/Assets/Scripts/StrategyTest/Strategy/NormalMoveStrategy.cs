using System.Collections.Generic;
using UnityEngine;

public class NormalMoveStrategy : IMoveStrategy
{
    private const float MOVE_DURATION = 0.15f;

    public MoveResult TryMove(
        Vector2Int currentPosition,
        Vector2Int moveDirection,
        GridManager gridManager
        )
    {
        Vector2Int nextPosition = currentPosition + moveDirection;

        if (gridManager.HasBox(nextPosition))
        {
            return MoveResult.Fail(currentPosition);
        }

        if (!gridManager.CanEnter(nextPosition, PlayerType.Normal))
        {
            return MoveResult.Fail(currentPosition);
        }

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
