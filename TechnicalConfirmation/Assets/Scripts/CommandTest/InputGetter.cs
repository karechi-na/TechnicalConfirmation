using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;

public class InputGetter : SingletonMonoBehaviour<InputGetter>
{
    public event Action<List<Direction>> OnReplayRequested;

    private Stack<ICommand> undoStack = new Stack<ICommand>();
    private Stack<ICommand> redoStack = new Stack<ICommand>();

    // Ghostのシミュレート用
    private List<Direction> directions = new List<Direction>();

    private PlayerInput playerInput;

    private Simulate simulate;

    [SerializeField] private CommandViewer commandViewer;
    [SerializeField] private PlayerViewer playerViewer;

    private bool inpputPossible = false;

    protected override void Awake()
    {
        base.Awake();
        playerInput = GetComponent<PlayerInput>();
    }

    void Start()
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

        playerInput.actions["Move"].canceled += StickInputCanceled;
    }

    private void OnDisable()
    {
        if (playerInput == null) return;

        playerInput.actions["Move"].performed -= OnMove;
        playerInput.actions["Undo"].performed -= OnUndo;
        playerInput.actions["Redo"].performed -= OnRedo;
        playerInput.actions["GhostSimulate"].performed -= OnGhostSimulate;

        playerInput.actions["Move"].canceled -= StickInputCanceled;
    }
    #endregion

    /// <summary>
    /// Moveイベントメソッド
    /// </summary>
    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.action.name != "Move") return;

        if (inpputPossible) return;

        Vector2 direction = context.ReadValue<Vector2>();
        if (direction.magnitude < 0.2) return;


        Direction dir = GetDirection(direction);

        ICommand command = new MoveCommand(simulate, dir);


        command.Execute();
        undoStack.Push(command);
        redoStack.Clear();

        inpputPossible = true;
    }

    /// <summary>
    /// Undoイベントメソッド
    /// </summary>
    public void OnUndo(InputAction.CallbackContext context)
    {
        if (context.action.name != "Undo") return;

        if (undoStack.Count == 0) return;

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

        if(redoStack.Count == 0) return;

        ICommand command = redoStack.Pop();
        command.Execute();
        undoStack.Push(command);
    }

    public void OnGhostSimulate(InputAction.CallbackContext context)
    {
        List<Direction> history = new List<Direction>();

        foreach (var cmd in undoStack)
        {
            if (cmd is MoveCommand move)
            {
                history.Add(move.Direction);
            }
        }

        history.Reverse();

        OnReplayRequested?.Invoke(history);
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
    private void StickInputCanceled(InputAction.CallbackContext context)
    {
        inpputPossible = false;
    }

    /// <summary>
    /// 入力された値によって列挙型(Direction)を返すメソッド
    /// </summary>
    private Direction GetDirection(Vector2 direction)
    {
        Direction dir = Direction.None;
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
