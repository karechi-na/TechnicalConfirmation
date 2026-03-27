using UnityEngine;

public class GhostMoveStrategy : IMoveStrategy
{
    private const float MOVE_DISTANCE = 1.0f;

    public void Move(Transform player, Vector2 input)
    {
        Vector3 move = new Vector3(input.x, 0.0f, input.y) * MOVE_DISTANCE;
        player.position += move;
    }
}
