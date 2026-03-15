using System;
using UnityEngine;

public class Simulate
{
    private Vector2 position = Vector2.zero;
    public event Action<Vector2> OnPositionChanged;

    public void Move(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                position += Vector2.up;
                break;
            case Direction.Down:
                position += Vector2.down;
                break;
            case Direction.Left:
                position += Vector2.left;
                break;
            case Direction.Right:
                position += Vector2.right;
                break;
        }

        OnPositionChanged?.Invoke(position);
    }
}
