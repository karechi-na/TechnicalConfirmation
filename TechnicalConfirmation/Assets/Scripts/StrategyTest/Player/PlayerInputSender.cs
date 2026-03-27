using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSender : SingletonMonoBehaviour<PlayerInputSender>
{
    private PlayerInput playerInput = null;
    private bool canMoveInput = true;

    public event Action<Vector2> OnMove;
    public event Action OnNormalSelected;
    public event Action OnGhostSelected;
    public event Action OnHeavySelected;

    protected override void Awake()
    {
        base.Awake();
        playerInput = GetComponent<PlayerInput>();
    }

    #region ÉCÉxÉìÉgìoò^ÅAâèú
    private void OnEnable()
    {
        playerInput.actions["Move"].performed += OnMovePerformed;
        playerInput.actions["Move"].canceled += OnMoveCanceled;

        playerInput.actions["Normal"].performed += OnNormalPerformed;
        playerInput.actions["Ghost"].performed += OnGhostPerformed;
        playerInput.actions["Heavy"].performed += OnHeavyPerformed;
    }

    private void OnDisable()
    {
        playerInput.actions["Move"].performed -= OnMovePerformed;
        playerInput.actions["Move"].canceled -= OnMoveCanceled;

        playerInput.actions["Normal"].performed -= OnNormalPerformed;
        playerInput.actions["Ghost"].performed -= OnGhostPerformed;
        playerInput.actions["Heavy"].performed -= OnHeavyPerformed;
    }
    #endregion

    private void OnMovePerformed(InputAction.CallbackContext callbackContext)
    {
        if (!canMoveInput) return;

        Vector2 inputValue = callbackContext.ReadValue<Vector2>();
        Vector2 direction = ConvertTo4Direction(inputValue);

        if (direction == Vector2.zero) return;

        canMoveInput = false;
        OnMove?.Invoke(direction);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        canMoveInput = true;
    }

    private void OnNormalPerformed(InputAction.CallbackContext callbackContext)
    {
        OnNormalSelected?.Invoke();
    }

    private void OnGhostPerformed(InputAction.CallbackContext callbackContext)
    {
        OnGhostSelected?.Invoke();
    }

    private void OnHeavyPerformed(InputAction.CallbackContext callbackContext)
    {
        OnHeavySelected?.Invoke();
    }

    private Vector2 ConvertTo4Direction(Vector2 inputValue)
    {
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
