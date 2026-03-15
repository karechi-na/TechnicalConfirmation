using UnityEngine;

public class AttackCommand : ICommand
{
    Player player;

    public AttackCommand(Player player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.Attack();
    }

    public void Undo()
    {

    }
}
