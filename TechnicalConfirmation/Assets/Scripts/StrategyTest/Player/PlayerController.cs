using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IMoveStrategy moveStrategy;

    private void Awake()
    {
        moveStrategy = new NormalMoveStrategy();
    }

    #region イベント登録、解除
    private void OnEnable()
    {
        PlayerInputSender.Instance.OnMove += Move;
        PlayerInputSender.Instance.OnNormalSelected += ChangeToNormal;
        PlayerInputSender.Instance.OnGhostSelected += ChangeToGhost;
        PlayerInputSender.Instance.OnHeavySelected += ChangeToHeavy;
    }

    private void OnDisable()
    {
        PlayerInputSender.Instance.OnMove -= Move;
        PlayerInputSender.Instance.OnNormalSelected -= ChangeToNormal;
        PlayerInputSender.Instance.OnGhostSelected -= ChangeToGhost;
        PlayerInputSender.Instance.OnHeavySelected -= ChangeToHeavy;
    }
    #endregion

    private void Move(Vector2 inputValue)
    {
        moveStrategy.Move(transform, inputValue);
    }

    private void ChangeToNormal()
    {
        moveStrategy = new NormalMoveStrategy();
    }

    private void ChangeToGhost()
    {
        moveStrategy = new GhostMoveStrategy();
    }

    private void ChangeToHeavy() 
    {
        moveStrategy = new HeavyMoveStrategy();
    }
}
