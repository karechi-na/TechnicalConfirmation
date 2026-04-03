using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// タイトルのメニューを管理するクラス
/// </summary>
public class TitleMenuView : MonoBehaviour
{
    [Header("ButtonComponentのアタッチされたGameObject")]
    [SerializeField] private GameObject[] buttons = null;

    // PlayerInputComponent
    private PlayerInput playerInput;

    /// <summary>
    /// 選択中のボタンを管理する列挙型
    /// </summary>
    private enum SelectButton
    {
        None,
        Button1,
        Button2,
        Button3
    }
    // 選択中のボタンを管理する変数
    private SelectButton selectButton = SelectButton.None;

    // 入力が処理されたかどうかを管理するフラグ
    private bool isPerformed = false;

    /// <summary>
    /// 初期化
    /// </summary>
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        ButtonImageEnableOff();
    }

    #region InputActionのイベント登録と解除
    private void OnEnable()
    {
        if (playerInput == null) return;

        playerInput.actions["Navigate"].performed += OnNavigatePerformed;
        playerInput.actions["Submit"].canceled += OnSubmit;
    }
    private void OnDisable()
    {
        playerInput.actions["Navigate"].performed -= OnNavigatePerformed;
        playerInput.actions["Submit"].canceled -= OnSubmit;
    }
    #endregion

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        Vector2 input = playerInput.actions["Navigate"].ReadValue<Vector2>();

        // 上下の入力が一定の閾値を超えていない場合、isPerformedをfalseにリセットして次の入力を受け付けるようにする
        if (Mathf.Abs(input.y) < 0.2f)
        {
            isPerformed = false;
        }
    }

    /// <summary>
    /// InputAction "Navigate" のperformedイベントのコールバック
    /// </summary>
    private void OnNavigatePerformed(InputAction.CallbackContext callbackContext)
    {
        Vector2 input = callbackContext.ReadValue<Vector2>();

        // 上下の入力が一定の閾値を超えていない場合は処理しない
        if (Mathf.Abs(input.y) < 0.4f) return;

        // 閾値を超える入力があった場合、isPerformedがtrueであれば処理しない（連続入力防止）
        if (isPerformed) return;

        // 上下の入力に応じて選択中のボタンを変更
        if (input.y > 0.0f)
        {
            MovePrev();
        }
        else if (input.y < 0.0f)
        {
            MoveNext();
        }

        // 選択中のボタンを強調表示
        if (selectButton != SelectButton.None)
        {
            ButtonDisplay(selectButton);
        }

        // 入力が処理されたことを示すフラグを立てる
        isPerformed = true;
    }

    /// <summary>
    /// InputAction "Submit" のcanceledイベントのコールバック
    /// </summary>
    private void OnSubmit(InputAction.CallbackContext callbackContext)
    {
        // 選択中のボタンがない場合は処理しない
        if (selectButton == SelectButton.None) return;

        // 選択中のボタンに対応するButtonコンポーネントのonClickイベントを呼び出す
        int index = (int)selectButton - 1;

        Button btn = buttons[index].GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.Invoke();
        }
    }

    /// <summary>
    /// 次のボタンを選択する
    /// </summary>
    private void MoveNext()
    {
        if (selectButton == SelectButton.None)
        {
            selectButton = SelectButton.Button1;
            return;
        }

        selectButton = (SelectButton)(((int)selectButton % 3) + 1);
    }

    /// <summary>
    /// 前のボタンを選択する
    /// </summary>
    private void MovePrev()
    {
        if (selectButton == SelectButton.None)
        {
            selectButton = SelectButton.Button3;
            return;
        }

        int index = (int)selectButton - 1;

        if (index < 1)
        {
            selectButton = SelectButton.Button3;
        }
        else
        {
            selectButton = (SelectButton)index;
        }
    }

    /// <summary>
    /// ButtonコンポーネントのImageを切る
    /// </summary>
    private void ButtonImageEnableOff()
    {
        foreach (var btn in buttons)
        {
            Image image = btn.GetComponent<Image>();
            if (image != null)
            {
                image.enabled = false;
            }
        }
    }

    /// <summary>
    /// 選択中のボタンを強調表示
    /// </summary>
    private void ButtonDisplay(SelectButton selectButton)
    {
        ButtonImageEnableOff();

        // 選択中のボタンに対応するButtonコンポーネントのImageを有効にする
        int index = (int)selectButton - 1;

        if (index < 0 || index >= buttons.Length) return;

        Image image = buttons[index].GetComponent<Image>();
        if (image != null)
        {
            image.enabled = true;
        }
    }
}
