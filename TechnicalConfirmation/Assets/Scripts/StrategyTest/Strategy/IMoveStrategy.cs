using UnityEngine;

public interface IMoveStrategy
{
    Vector2Int GetNextPosition(Vector2Int currentPosition, Vector2Int moveDirection);
}
