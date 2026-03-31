using System.Collections.Generic;
using UnityEngine;

public class MoveResult
{
    public bool CanMove { get; private set; }
    public Vector2Int TargetPosition { get; private set; }
    public List<Vector2Int> Route { get; private set; }
    public float MoveDuration { get; private set; }

    public bool HasBoxMove { get; private set; }
    public Vector2Int BoxCurrentPosition { get; private set; }
    public Vector2Int BoxTargetPosition { get; private set; }

    public MoveResult(
        bool canMove, 
        Vector2Int targetPosition, 
        List<Vector2Int> route,
        float moveDuration,
        bool hasBoxMove = false,
        Vector2Int boxCurrentPosition = default,
        Vector2Int boxTargetPosition = default)
    {
        CanMove = canMove;
        TargetPosition = targetPosition;
        Route = route;
        MoveDuration = moveDuration;
        HasBoxMove = hasBoxMove;
        BoxCurrentPosition = boxCurrentPosition;
        BoxTargetPosition = boxTargetPosition;
    }

    public static MoveResult Fail(Vector2Int currentPosition)
    {
        return new MoveResult(
            false,
            currentPosition,
            null,
            0.0f
            );
    }
}
