using UnityEngine;

public class GhostMoveStrategy : IMoveStrategy
{
    public Vector2Int GetNextPosition(Vector2Int currentPosition, Vector2Int moveDirection)
    {
        return currentPosition + moveDirection;
    }
}
