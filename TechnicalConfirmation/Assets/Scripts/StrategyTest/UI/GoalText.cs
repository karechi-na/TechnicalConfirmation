using UnityEngine;
using TMPro;

namespace StrategyTest.UI
{
    /// <summary>
    /// GoalTextクラスは、プレイヤーがゴールタイルに入ったときにゴールテキストを表示するためのクラスです。
    /// </summary>
    public class GoalText : MonoBehaviour
    {
        [Header("References")]
        [Header("ゴールテキストを表示するTextMeshProUGUIコンポーネント")]
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;

        [Header("ゴールのタイル")]
        [SerializeField] private ExitTile exitTile;

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Start()
        {
            textMeshProUGUI.enabled = false;
        }

        #region イベントの登録と解除
        private void OnEnable()
        {
            exitTile.OnPlayerEnter += OnPlayerEnter;
        }
        private void OnDisable() 
        {
            exitTile.OnPlayerEnter -= OnPlayerEnter;
        }
        #endregion

        /// <summary>
        /// プレイヤーがゴールタイルに入ったときの処理
        /// TextMeshProUGUIを有効にしてゴールテキストを表示する
        /// </summary>
        private void OnPlayerEnter(bool isEntered)
        {
            if (isEntered)
            {
                textMeshProUGUI.enabled = true;
            }
        }
    }
}