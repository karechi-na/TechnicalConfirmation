using System.Collections.Generic;
using UnityEngine;

public class GhostMoveStrategy : IMoveStrategy
{
    private const float MOVE_DURATION = 0.35f;

    public MoveResult TryMove(
        Vector2Int currentPosition,
        Vector2Int moveDirection,
        GridManager gridManager
        )
    {
        Vector2Int checkPosition = currentPosition + moveDirection;
        List<Vector2Int> route = new List<Vector2Int>();

        while (true)
        {
            if (!gridManager.ExistsTile(checkPosition))
            {
                return MoveResult.Fail(currentPosition);
            }

            if (gridManager.IsFence(checkPosition))
            {
                route.Add(checkPosition);
                checkPosition += moveDirection; 
                continue;
            }

            if (gridManager.HasBox(checkPosition))
            {
                return MoveResult.Fail(currentPosition);
            }

            if (!gridManager.CanEnter(checkPosition, PlayerType.Ghost))
            {
                return MoveResult.Fail(currentPosition);
            }

            route.Add(checkPosition);

            return new MoveResult(
                true,
                checkPosition,
                route,
                MOVE_DURATION
                );
        }
    }
}
