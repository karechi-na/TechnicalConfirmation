using System.Collections.Generic;
using UnityEngine;

public class HeavyMoveStrategy: IMoveStrategy
{
    private const float MOVE_DURATION = 0.22f;

    public MoveResult TryMove(
        Vector2Int currentPosition,
        Vector2Int moveDirection,
        GridManager gridManager
        )
    {
        Vector2Int nextPosition = currentPosition + moveDirection;

        if (gridManager.HasBox(nextPosition))
        {
            Vector2Int boxTargetPosition = nextPosition + moveDirection;

            if (!gridManager.CanBoxMoveTo(boxTargetPosition))
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
                MOVE_DURATION,
                true,
                nextPosition,
                boxTargetPosition);
        }

        if (!gridManager.CanEnter(nextPosition, PlayerType.Heavy))
        {
            return MoveResult.Fail(currentPosition);
        }

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
