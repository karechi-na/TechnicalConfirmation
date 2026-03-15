using UnityEngine;
using UnityEngine.InputSystem;

public class GamePadInputAcquisition : SingletonMonoBehaviour<GamePadInputAcquisition>
{
    public Gamepad gamePad;
    public Keyboard keyboard;

    public Vector2 leftStick {  get; private set; }
    public Vector2 rightStick { get; private set; }

    public bool southButtonDown { get; private set; }
    public bool southButton { get; private set; }
    public bool southButtonUp { get; private set; }
   

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        gamePad = Gamepad.current;
        keyboard = Keyboard.current;

        if (gamePad == null)
        {
            ResetInput();
            return;
        }

        // スティック
        leftStick = gamePad.leftStick.ReadValue();
        rightStick = gamePad.rightStick.ReadValue();

        bool abuttonDown = gamePad?.buttonSouth.wasPressedThisFrame ?? false;
        bool fkeyDown = keyboard?.fKey.wasPressedThisFrame ?? false;

        // Aボタン
        southButtonDown = abuttonDown || fkeyDown;
        southButton = gamePad.buttonSouth.isPressed;
        southButtonUp = gamePad.buttonSouth.wasReleasedThisFrame;
    }
    private void ResetInput()
    {
        leftStick = Vector2.zero;
        rightStick = Vector2.zero;

        southButtonDown = false;
        southButton = false;
        southButtonUp = false;
    }
}
