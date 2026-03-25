using UnityEngine;
using TMPro;

/// <summary>
/// ゴール時に表示するテキストの状態を切り替えるクラス
/// </summary>
public class GoalText : MonoBehaviour
{
    [Header("ゴール時に表示するテキスト")]
    [SerializeField] private TextMeshProUGUI goalText = null;

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
        goalText.enabled = false;
    }

    #region イベント登録、解除
    private void OnEnable()
    {
        InGameManager.Instance.OnExit += TextChange;
    }
    private void OnDisable()
    {
        InGameManager.Instance.OnExit -= TextChange;
    }
    #endregion

    /// <summary>
    /// テキストの表示状態を切り替えるメソッド
    /// </summary>
    private void TextChange(bool isGoal)
    {
        if (isGoal) goalText.enabled = true;
        else goalText.enabled = false;
    }
}
