using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTile : GridTileBase
{
    [Header("ExitTile Settings")]

    [Header("0:通常のマテリアル, 1:スイッチが押されたときのマテリアル")]
    [SerializeField] private Material[] materials = new Material[2];

    [Header("子オブジェクトのMeshRenderer")]
    [SerializeField] private MeshRenderer meshRenderer;

    [Header("HeavySwitchTileの参照")]
    [SerializeField] private HeavySwitchTile heavySwitchTile;

    [Header("SceneReference")]
    [SerializeField] private SceneReference sceneReference;

    // プレイヤーがゴールに入ったときのイベント
    public event Action<bool> OnPlayerEnter;

    // ゴールにできるかどうかのフラグ
    private bool canGoal = false;

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
        MaterialChanged();
    }

    #region イベントの登録と解除
    private void OnEnable()
    {
        heavySwitchTile.OnHeavySwitchActivated += OnSwitchChanged;
    }

    private void OnDisable()
    {
        heavySwitchTile.OnHeavySwitchActivated -= OnSwitchChanged;
    }
    #endregion

    /// <summary>
    /// ゴールに入れるかどうかの判定を行うメソッド
    /// 全状態のプレイヤーが入れるようにするため、常にtrueを返す
    /// </summary>
    public override bool CanEnter(PlayerType playerType)
    {
        return true;
    }

    /// <summary>
    /// 全状態のプレイヤーが入れるが、ゴールできるのはNormal状態のときだけにする
    /// </summary>
    public override void OnEnter(PlayerController player)
    {
        // ゴールできる状態でなければ何もしない
        if (!canGoal) return;
        // Normal状態以外のときはゴールできないようにする
        if (player.CurrentType != PlayerType.Normal) return;

        // ゴールしたときの処理をするよう通知する
        OnPlayerEnter?.Invoke(true);
        // タイトルに戻る処理を呼び出す
        Invoke(nameof(BackToTitle), 1.5f);
    }
    /// <summary>
    /// ボタンの状態が変わったときに発火するメソッド
    /// </summary>
    public void OnSwitchChanged(bool isPressed)
    {
        // ゴールにできるかどうかのフラグを切り替える
        canGoal = isPressed;

        // マテリアルを切り替える
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

    /// <summary>
    /// タイトルに戻る処理をするメソッド
    /// </summary>
    private void BackToTitle()
    {
        SceneLoader.Instance.LoadTitleScene();
    }
}
