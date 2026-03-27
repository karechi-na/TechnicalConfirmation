using UnityEngine;

public class HeavySwitchTile : MonoBehaviour, IGridTile
{
    public bool CanEnter(PlayerType playerType)
    {
        return true;
    }

    public void OnEnter(PlayerController player)
    {
        if (player.CurrentType != PlayerType.Heavy) return;

        Debug.Log("HeavyスイッチON");
    }
}
