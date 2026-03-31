using UnityEngine;

public interface IMoveStrategy
{
    MoveResult TryMove(
        Vector2Int currentPosition,
        Vector2Int moveDirection,
        GridManager gridManager);
}
