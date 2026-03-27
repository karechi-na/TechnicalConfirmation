using UnityEngine;

public interface IMoveStrategy
{
    void Move(Transform player, Vector2 input);
}
