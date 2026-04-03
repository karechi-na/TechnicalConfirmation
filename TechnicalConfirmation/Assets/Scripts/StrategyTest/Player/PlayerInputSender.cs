using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 入力を受け取り、イベントとして発火するクラス
/// </summary>
public class PlayerInputSender : MonoBehaviour
{
    // PlayerInputコンポーネントをキャッシュするための変数
    private PlayerInput playerInput = null;
    // 移動入力の処理を制御するフラグ
    private bool canMoveInput = true;

    // イベント宣言
    public event Action<Vector2> OnMove;    // 移動入力があったときに発火するイベント
    public event Action OnNormalSelected;   // Normalタイプが選択されたときに発火するイベント
    public event Action OnGhostSelected;    // Ghostタイプが選択されたときに発火するイベント
    public event Action OnHeavySelected;    // Heavyタイプが選択されたときに発火するイベント

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    #region イベント登録、解除
    private void OnEnable()
    {
        playerInput.actions["Move"].performed += OnMovePerformed;
        playerInput.actions["Move"].canceled += OnMoveCanceled;

        playerInput.actions["Normal"].performed += OnNormalPerformed;
        playerInput.actions["Ghost"].performed += OnGhostPerformed;
        playerInput.actions["Heavy"].performed += OnHeavyPerformed;

        playerInput.actions["TitleBack"].canceled += OnTitleBackCanceled;
    }

    private void OnDisable()
    {
        playerInput.actions["Move"].performed -= OnMovePerformed;
        playerInput.actions["Move"].canceled -= OnMoveCanceled;

        playerInput.actions["Normal"].performed -= OnNormalPerformed;
        playerInput.actions["Ghost"].performed -= OnGhostPerformed;
        playerInput.actions["Heavy"].performed -= OnHeavyPerformed;

        playerInput.actions["TitleBack"].canceled -= OnTitleBackCanceled;
    }
    #endregion

    /// <summary>
    /// 移動入力があったときに呼び出されるメソッド
    /// </summary>
    private void OnMovePerformed(InputAction.CallbackContext callbackContext)
    {
        // 移動入力の処理が許可されていない場合は、処理を中断する
        if (!canMoveInput) return;

        // 入力された値を取得し、4方向のベクトルに変換する
        Vector2 inputValue = callbackContext.ReadValue<Vector2>();
        Vector2 direction = ConvertTo4Direction(inputValue);

        // 方向がゼロベクトルの場合は、処理を中断する
        if (direction == Vector2.zero) return;

        // 移動入力の処理を一時的に無効化し、イベントを発火する
        canMoveInput = false;
        OnMove?.Invoke(direction);
    }

    /// <summary>
    /// 移動入力がキャンセルされたときに呼び出されるメソッド
    /// </summary>
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        // 移動入力の処理を再度許可する
        canMoveInput = true;
    }

    /// <summary>
    /// Normalタイプが選択されたときに呼び出されるメソッド
    /// </summary>
    private void OnNormalPerformed(InputAction.CallbackContext callbackContext)
    {
        // Normalタイプが選択されたときにイベントを発火する
        OnNormalSelected?.Invoke();
    }

    /// <summary>
    /// Ghostタイプが選択されたときに呼び出されるメソッド
    /// </summary>
    private void OnGhostPerformed(InputAction.CallbackContext callbackContext)
    {
        // Ghostタイプが選択されたときにイベントを発火する
        OnGhostSelected?.Invoke();
    }

    /// <summary>
    /// Heavyタイプが選択されたときに呼び出されるメソッド
    /// </summary>
    private void OnHeavyPerformed(InputAction.CallbackContext callbackContext)
    {
        // Heavyタイプが選択されたときにイベントを発火する
        OnHeavySelected?.Invoke();
    }

    /// <summary>
    /// タイトルに戻るキー入力があったときに呼び出されるメソッド
    /// </summary>
    private void OnTitleBackCanceled(InputAction.CallbackContext callbackContext)
    {
        // タイトルに戻る入力があったときにタイトルシーンに遷移する
        SceneLoader.Instance.LoadTitleScene();
    }

    /// <summary>
    /// 入力されたベクトルを4方向のベクトルに変換するメソッド
    /// </summary>
    private Vector2 ConvertTo4Direction(Vector2 inputValue)
    {
        // 入力されたベクトルのx成分とy成分の絶対値を比較し、大きい方に応じて4方向のベクトルを返す
        if (Mathf.Abs(inputValue.x) > Mathf.Abs(inputValue.y))
        {
            return new Vector2(Mathf.Sign(inputValue.x), 0.0f);
        }

        if (Mathf.Abs(inputValue.y) > 0.0f)
        {
            return new Vector2(0.0f, Mathf.Sign(inputValue.y));
        }

        return Vector2.zero;
    }
}
