using UnityEngine;

public class NormalMoveStrategy : IMoveStrategy
{
    public Vector2Int GetNextPosition(Vector2Int currentPosition, Vector2Int moveDirection)
    {
        return currentPosition + moveDirection;
    }
}
