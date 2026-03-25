using UnityEngine;

/// <summary>
/// 出口が行う機能を実装するクラス
/// </summary>
public class Exit : MonoBehaviour
{
    [Header("出口のMeshRenderer")]
    [SerializeField] private MeshRenderer meshRenderer = null;

    [Header("切り替える色を登録するMaterial配列")]
    [SerializeField] private Material[] material = null;

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
        MaterialChanged();
    }

    #region イベント登録、解除
    private void OnEnable()
    {
        SwitchManager.Instance.OnSwitchStateChanged += OnSwitchChanged;
    }

    private void OnDisable()
    {
        SwitchManager.Instance.OnSwitchStateChanged -= OnSwitchChanged;
    }
    #endregion

    /// <summary>
    /// ボタンの状態が変わったときに発火するメソッド
    /// </summary>
    public void OnSwitchChanged(bool isPressed)
    {
        int changeNum = isPressed ? 1 : 0;
        MaterialChanged(changeNum);
    }

    /// <summary>
    /// Materialを切り替えるメソッド
    /// デフォルト引数として初期化時のMaterialの番地を指定
    /// </summary>
    private void MaterialChanged(int matNum = 0)
    {
        meshRenderer.material = material[matNum];
    }
}
