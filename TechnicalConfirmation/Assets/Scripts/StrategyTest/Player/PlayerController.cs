using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float modelYOffset = 0.5f;

    // プレイヤーの状態を管理する読み取り専用変数
    public PlayerType CurrentType { get; private set; } = PlayerType.Normal;

    // 移動戦略を管理する変数
    private IMoveStrategy moveStrategy;

    public Vector2Int CurrentGridPosition { get; private set; }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Awake()
    {
        moveStrategy = new NormalMoveStrategy();
    }

    private void Start()
    {
        transform.position = GridManager.Instance.GridToWorldPosition(CurrentGridPosition, modelYOffset);
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
        Vector2Int moveDirection = new Vector2Int((int)inputValue.x, (int)inputValue.y);

        MoveResult result = moveStrategy.TryMove(CurrentGridPosition, moveDirection, GridManager.Instance);

        if (!result.CanMove) return;

        if (result.HasBoxMove)
        {
            BoxTile box = GridManager.Instance.GetBox(result.BoxCurrentPosition);
            if (box != null)
            {
                box.MoveTo(result.BoxTargetPosition);
            }
        }

        CurrentGridPosition = result.TargetPosition;
        transform.position = GridManager.Instance.GridToWorldPosition(CurrentGridPosition, modelYOffset);

        var tiles = GridManager.Instance.GetTiles(CurrentGridPosition);
        foreach (GridTileBase tile in tiles)
        {
            tile.OnEnter(this);
        }

    }

    #region Strategy変更
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
    #endregion

    private void LogOutput(PlayerType playerType)
    {
        Debug.Log("PlayerType : " + playerType);
    }
}
