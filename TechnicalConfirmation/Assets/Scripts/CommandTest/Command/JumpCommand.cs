using UnityEngine;

public class JumpCommand : ICommand
{
    Player player;

    public JumpCommand(Player player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.Jump();
    }

    public void Undo()
    {

    }
}
