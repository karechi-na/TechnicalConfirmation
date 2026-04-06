using System.Diagnostics;
using UnityEngine;
using Debug=UnityEngine.Debug;

/// <summary>
/// PlayerControllerはプレイヤーの状態を管理し、プレイヤーの移動を制御するクラスです。
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInputSender playerInputSender = null; // PlayerInputSenderへの参照

    [Header("モデルのYオフセット")]
    [SerializeField] private float modelYOffset = 0.5f;

    [Header("プレイヤーの状態に応じたマテリアル")]
    [SerializeField] private Material[] playerMaterials = null; // プレイヤーの状態に応じたマテリアルの配列 

    // プレイヤーの状態を管理する読み取り専用変数
    public PlayerType CurrentType { get; private set; } = PlayerType.Normal;

    // 移動戦略を管理する変数
    private IMoveStrategy moveStrategy;

    // プレイヤーの現在のグリッド位置を管理する変数
    public Vector2Int CurrentGridPosition { get; private set; }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Awake()
    {
        // 初期の移動戦略をNormalMoveStrategyに設定
        ChangeToNormal();
    }

    /// <summary>
    /// 参照の取得と初期位置の設定
    /// </summary>
    private void Start()
    {
        transform.position = GridManager.Instance.GridToWorldPosition(CurrentGridPosition, modelYOffset);
    }

    #region イベント登録、解除
    private void OnEnable()
    {
        playerInputSender.OnMove += Move;
        playerInputSender.OnNormalSelected += ChangeToNormal;
        playerInputSender.OnGhostSelected += ChangeToGhost;
        playerInputSender.OnHeavySelected += ChangeToHeavy;
    }

    private void OnDisable()
    {
        playerInputSender.OnMove -= Move;
        playerInputSender.OnNormalSelected -= ChangeToNormal;
        playerInputSender.OnGhostSelected -= ChangeToGhost;
        playerInputSender.OnHeavySelected -= ChangeToHeavy;
    }
    #endregion

    /// <summary>
    /// InputSenderからの移動イベントを処理するメソッド。現在の移動戦略に基づいてプレイヤーを移動させる。
    /// </summary>
    private void Move(Vector2 inputValue)
    {
        // 入力値を整数のグリッド移動方向に変換
        Vector2Int moveDirection = new Vector2Int((int)inputValue.x, (int)inputValue.y);

        // 現在の移動Strategyを使用して移動できるかを判定
        MoveResult result = moveStrategy.TryMove(CurrentGridPosition, moveDirection, GridManager.Instance);

        // 移動できない場合は処理を終了
        if (!result.CanMove) return;

        // 移動する場合、Boxも一緒に移動する必要があるかを判定し、必要ならBoxも移動させる
        if (result.HasBoxMove)
        {
            BoxTile box = GridManager.Instance.GetBox(result.BoxCurrentPosition);
            if (box != null)
            {
                box.MoveTo(result.BoxTargetPosition);
            }
        }

        // プレイヤーを判定結果に基づいて移動させる
        CurrentGridPosition = result.TargetPosition;
        transform.position = GridManager.Instance.GridToWorldPosition(CurrentGridPosition, modelYOffset);

        // 移動後のタイルに対してOnEnterを呼び出す
        var tiles = GridManager.Instance.GetTiles(CurrentGridPosition);
        foreach (GridTileBase tile in tiles)
        {
            tile.OnEnter(this);
        }

    }

    #region Strategy変更
    /// <summary>
    /// Normalタイプに変更するメソッド。移動戦略をNormalMoveStrategyに切り替える。
    /// </summary>
    private void ChangeToNormal()
    {
        CurrentType = PlayerType.Normal;

        moveStrategy = new NormalMoveStrategy();

        // マテリアルの変更
        MaterialChange();

        LogOutput(CurrentType);
    }

    /// <summary>
    /// Ghostタイプに変更するメソッド。移動戦略をGhostMoveStrategyに切り替える。
    /// </summary>
    private void ChangeToGhost()
    {
        CurrentType = PlayerType.Ghost;

        moveStrategy = new GhostMoveStrategy();

        // マテリアルの変更
        MaterialChange();

        LogOutput(CurrentType);
    }

    /// <summary>
    /// Heavyタイプに変更するメソッド。移動戦略をHeavyMoveStrategyに切り替える。
    /// </summary>
    private void ChangeToHeavy() 
    {
        CurrentType = PlayerType.Heavy;

        moveStrategy = new HeavyMoveStrategy();

        // マテリアルの変更
        MaterialChange();

        LogOutput(CurrentType);
    }
    #endregion

    /// <summary>
    /// materialをCurrentTypeに応じて変更するメソッド。MeshRendererコンポーネントが存在する場合に、対応するマテリアルを適用する。
    /// </summary>
    private void MaterialChange()
    {
        if (TryGetComponent<MeshRenderer>(out MeshRenderer mesh))
        {
            mesh.material = playerMaterials[(int)CurrentType];
        }
    }

    /// <summary>
    /// デバッグ用のログ出力メソッド。UNITY_EDITORでのみ有効。
    /// </summary>
    [Conditional("UNITY_EDITOR")]
    private void LogOutput(PlayerType playerType)
    {
        Debug.Log("PlayerType : " + playerType);
    }
}
