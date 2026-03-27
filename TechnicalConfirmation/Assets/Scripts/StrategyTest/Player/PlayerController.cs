using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerType CurrentType { get; private set; } = PlayerType.Normal;

    private IMoveStrategy moveStrategy;

    public Vector2Int CurrentGridPosition { get; private set; }

    private void Awake()
    {
        moveStrategy = new NormalMoveStrategy();
    }

    private void Start()
    {
        transform.position = GridManager.Instance.GridToWorldPosition(CurrentGridPosition);
    }

    #region ÉCÉxÉìÉgìoò^ÅAâèú
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
        Vector2Int moveDirection = new Vector2Int((int)inputValue.x, (int)inputValue.y);
        Vector2Int nextGridPosition = moveStrategy.GetNextPosition(CurrentGridPosition, moveDirection);

        IGridTile tile = GridManager.Instance.GetTile(nextGridPosition);
        if (tile == null) return;

        CurrentGridPosition = nextGridPosition;
        transform.position = GridManager.Instance.GridToWorldPosition(CurrentGridPosition);

        tile.OnEnter(this);
    }

    private void ChangeToNormal()
    {
        CurrentType = PlayerType.Normal;
        moveStrategy = new NormalMoveStrategy();
        LogOutput(CurrentType);
    }

    private void ChangeToGhost()
    {
        CurrentType = PlayerType.Ghost;
        moveStrategy = new GhostMoveStrategy();
        LogOutput(CurrentType);
    }

    private void ChangeToHeavy() 
    {
        CurrentType = PlayerType.Heavy;
        moveStrategy = new HeavyMoveStrategy();
        LogOutput(CurrentType);
    }

    private void LogOutput(PlayerType playerType)
    {
        Debug.Log("PlayerType : " + playerType);
    }
}
