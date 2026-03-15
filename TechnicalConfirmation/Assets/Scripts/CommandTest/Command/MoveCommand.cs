using UnityEngine;

public class MoveCommand : ICommand
{
    private Simulate simulate;

    Direction direction = Direction.None;

    public MoveCommand(Simulate simulate, Direction direction)
    {
        this.simulate = simulate;
        this.direction = direction;
    }

    public void Execute()
    {
        simulate.Move(direction);
    }

    public void Undo()
    {
        simulate.Move(GetOpposite(direction));
    }

    private Direction GetOpposite(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up: return Direction.Down;
            case Direction.Down: return Direction.Up;
            case Direction.Left: return Direction.Right;
            case Direction.Right: return Direction.Left;
        }
        return Direction.None;
    }
}
