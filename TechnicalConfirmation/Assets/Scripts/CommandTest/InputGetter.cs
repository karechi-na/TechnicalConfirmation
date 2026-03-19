using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;

public class InputGetter : SingletonMonoBehaviour<InputGetter>
{
    // コマンドの履歴をリプレイするためのイベント
    public event Action<List<Direction>> OnReplayRequested;

    // Undoのためのスタック
    private Stack<ICommand> undoStack = new Stack<ICommand>();
    // Redoのためのスタック
    private Stack<ICommand> redoStack = new Stack<ICommand>();

    // PlayerInputコンポーネントへの参照
    private PlayerInput playerInput;

    // シミュレートクラスへの参照
    private Simulate simulate;

    [Header("コマンドとプレイヤーのビューへの参照")]
    [SerializeField] private CommandViewer commandViewer;
    [SerializeField] private PlayerViewer playerViewer;

    // 入力の多重登録を防止するフラグ
    private bool inpputPossible = false;

    /// <summary>
    /// AwakeでPlayerInputコンポーネントを取得
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        playerInput = GetComponent<PlayerInput>();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
        ReferenceInitialization();
    }

    #region イベント登録と解除
    private void OnEnable()
    {
        if (playerInput == null) return;

        playerInput.actions["Move"].performed += OnMove;
        playerInput.actions["Undo"].performed += OnUndo;
        playerInput.actions["Redo"].performed += OnRedo;
        playerInput.actions["GhostSimulate"].performed += OnGhostSimulate;

        playerInput.actions["Move"].canceled += OnStickInputCanceled;
    }

    private void OnDisable()
    {
        if (playerInput == null) return;

        playerInput.actions["Move"].performed -= OnMove;
        playerInput.actions["Undo"].performed -= OnUndo;
        playerInput.actions["Redo"].performed -= OnRedo;
        playerInput.actions["GhostSimulate"].performed -= OnGhostSimulate;

        playerInput.actions["Move"].canceled -= OnStickInputCanceled;
    }
    #endregion

    /// <summary>
    /// Moveイベントメソッド
    /// </summary>
    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.action.name != "Move") return;

        // 入力の多重登録を防止
        if (inpputPossible) return;

        // 入力された値を取得
        Vector2 direction = context.ReadValue<Vector2>();
        // 入力が小さい場合は無視
        if (direction.magnitude < 0.2) return;

        // 入力された値によって列挙型(Direction)を返す
        Direction dir = GetDirection(direction);

        // MoveCommandを作成して実行
        ICommand command = new MoveCommand(simulate, dir);

        // コマンドを実行して、Undoスタックに積む、Redoスタックはクリア
        command.Execute();
        undoStack.Push(command);
        redoStack.Clear();

        // 入力の多重登録を防止するフラグを立てる
        inpputPossible = true;
    }

    /// <summary>
    /// Undoイベントメソッド
    /// </summary>
    public void OnUndo(InputAction.CallbackContext context)
    {
        if (context.action.name != "Undo") return;

        // Undoスタックが空の場合は何もしない
        if (undoStack.Count == 0) return;

        // Undoスタックからコマンドを取り出して、Undoを実行、Redoスタックに積む
        ICommand command = undoStack.Pop();
        command.Undo();
        redoStack.Push(command);
    }

    /// <summary>
    /// Redoイベントメソッド
    /// </summary>
    public void OnRedo(InputAction.CallbackContext context)
    {
        if (context.action.name != "Redo") return;

        // Redoスタックが空の場合は何もしない
        if (redoStack.Count == 0) return;

        // Redoスタックからコマンドを取り出して、Executeを実行、Undoスタックに積む
        ICommand command = redoStack.Pop();
        command.Execute();
        undoStack.Push(command);
    }

    /// <summary>
    /// GhostSimulateイベントメソッド
    /// </summary>
    public void OnGhostSimulate(InputAction.CallbackContext context)
    {
        // UndoスタックからMoveCommandのDirectionを取り出して、リプレイ用のリストに追加
        List<Direction> history = new List<Direction>();

        // UndoスタックからMoveCommandのDirectionを取り出して、リプレイ用のリストに追加
        foreach (var cmd in undoStack)
        {
            if (cmd is MoveCommand move)
            {
                history.Add(move.Direction);
            }
        }

        // リプレイ用のリストを逆順にする
        history.Reverse();

        // リプレイ用のイベントを呼び出す
        OnReplayRequested?.Invoke(history);

        // Undoスタックをクリア
        undoStack.Clear();
    }

    /// <summary>
    /// 参照の初期化
    /// </summary>
    private void ReferenceInitialization()
    {
        simulate = new Simulate();
        commandViewer.Initialized(simulate);
        playerViewer.Initialized(simulate);
    }

    /// <summary>
    /// Moveの入力がキャンセルされた時のイベントメソッド
    /// </summary>
    private void OnStickInputCanceled(InputAction.CallbackContext context)
    {
        inpputPossible = false;
    }

    /// <summary>
    /// 入力された値によって列挙型(Direction)を返すメソッド
    /// </summary>
    private Direction GetDirection(Vector2 direction)
    {
        Direction dir = Direction.None;

        // 入力された値の絶対値が大きい方を優先して、列挙型(Direction)を返す
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            dir = direction.x > 0 ? Direction.Right : Direction.Left;
        }
        else
        {
            dir = direction.y > 0 ? Direction.Up : Direction.Down;
        }
        return dir;
    }
}
