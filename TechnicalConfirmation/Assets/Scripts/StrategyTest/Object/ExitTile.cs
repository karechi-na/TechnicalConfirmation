using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTile : GridTileBase
{
    [Header("ExitTile Settings")]

    [SerializeField] private Material[] materials = new Material[2];
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private HeavySwitchTile heavySwitchTile;

    [Header("SceneReference")]
    [SerializeField] private SceneReference sceneReference;

    public event Action<bool> OnPlayerEnter;

    private bool canGoal = false;

    private void Start()
    {
        MaterialChanged();
    }

    private void OnEnable()
    {
        heavySwitchTile.OnHeavySwitchActivated += OnSwitchChanged;
    }

    private void OnDisable()
    {
        heavySwitchTile.OnHeavySwitchActivated -= OnSwitchChanged;
    }

    public override bool CanEnter(PlayerType playerType)
    {
        return true;
    }
    public override void OnEnter(PlayerController player)
    {
        if (!canGoal) return;
        if (player.CurrentType == PlayerType.Ghost) return;

        OnPlayerEnter?.Invoke(true);
        Invoke(nameof(BackToTitle), 1.5f);
    }
    /// <summary>
    /// ボタンの状態が変わったときに発火するメソッド
    /// </summary>
    public void OnSwitchChanged(bool isPressed)
    {
        canGoal = isPressed;
        int changeNum = isPressed ? 1 : 0;
        MaterialChanged(changeNum);
    }

    /// <summary>
    /// Materialを切り替えるメソッド
    /// デフォルト引数として初期化時のMaterialの番地を指定
    /// </summary>
    private void MaterialChanged(int matNum = 0)
    {
        meshRenderer.material = materials[matNum];
    }
    private void BackToTitle()
    {
        SceneManager.LoadScene(sceneReference.TitleSceneName);
    }
}
