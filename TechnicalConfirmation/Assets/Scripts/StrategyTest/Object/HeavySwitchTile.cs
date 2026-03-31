using System;
using UnityEngine;

public class HeavySwitchTile : GridTileBase
{
    public event Action<bool> OnHeavySwitchActivated;
    private bool isActivated = false;

    public override bool CanEnter(PlayerType playerType)
    {
        return true;
    }

    public override void OnEnter(PlayerController player)
    {
        if (player.CurrentType != PlayerType.Heavy) return;

        Debug.Log("HeavyスイッチON");
        isActivated = !isActivated;
        OnHeavySwitchActivated?.Invoke(isActivated);
    }
}
