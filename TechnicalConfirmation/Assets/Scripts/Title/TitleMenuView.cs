using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TitleMenuView : MonoBehaviour
{
    [SerializeField] private GameObject[] buttons = null;

    private PlayerInput playerInput;

    private enum SelectButton
    {
        None,
        Button1,
        Button2,
        Button3
    }
    private SelectButton selectButton = SelectButton.None;

    private bool isPerformed = false;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        ButtonInitialize();
    }

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

    private void Update()
    {
        Vector2 input = playerInput.actions["Navigate"].ReadValue<Vector2>();

        if (Mathf.Abs(input.y) < 0.2f)
        {
            isPerformed = false;
        }
    }


    private void OnNavigatePerformed(InputAction.CallbackContext callbackContext)
    {
        Vector2 input = callbackContext.ReadValue<Vector2>();

        if (Mathf.Abs(input.y) < 0.4f) return;

        if (isPerformed) return;

        if (input.y > 0.0f)
        {
            MovePrev();
        }
        else if (input.y < 0.0f)
        {
            MoveNext();
        }

        Debug.Log("selectButton : " + selectButton);

        if (selectButton != SelectButton.None)
        {
            ButtonDisplay(selectButton);
        }

        isPerformed = true;
    }
    private void OnSubmit(InputAction.CallbackContext callbackContext)
    {
        if(selectButton == SelectButton.None) return;

        int index = (int)selectButton - 1;

        Button btn = buttons[index].GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.Invoke();
        }
    }

    private void MoveNext()
    {
        if (selectButton == SelectButton.None)
        {
            selectButton = SelectButton.Button1;
            return;
        }

        selectButton = (SelectButton)(((int)selectButton % 3) + 1);
    }

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
    private void ButtonInitialize()
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
        ButtonInitialize();

        int index = (int)selectButton - 1;

        if (index < 0 || index >= buttons.Length) return;

        Image image = buttons[index].GetComponent<Image>();
        if (image != null)
        {
            image.enabled = true;
        }
    }
}
