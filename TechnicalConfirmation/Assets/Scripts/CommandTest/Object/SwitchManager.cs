using System;
using UnityEngine;
using TechConf;

public class SwitchManager : SingletonMonoBehaviour<SwitchManager>
{
    [SerializeField] private Button button = null;

    public bool IsPressed { get; private set; }

    public event Action<bool> OnSwitchStateChanged;

    private void Update()
    {
        bool newState = button.IsPressed;

        if(newState == IsPressed) return;

        IsPressed = newState;
        OnSwitchStateChanged?.Invoke(IsPressed);
    }
}
