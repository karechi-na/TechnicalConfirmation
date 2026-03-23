using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer = null;

    [SerializeField] private Material[] material = null;

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
