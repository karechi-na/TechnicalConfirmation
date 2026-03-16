using UnityEngine;

public class MoveCommand : ICommand
{
    private Simulate simulate;

    public Direction Direction { get; private set; }

    public MoveCommand(Simulate simulate, Direction direction)
    {
        this.simulate = simulate;
        this.Direction = direction;
    }

    public void Execute()
    {
        simulate.Move(Direction);
    }

    public void Undo()
    {
        simulate.Move(GetOpposite(Direction));
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
