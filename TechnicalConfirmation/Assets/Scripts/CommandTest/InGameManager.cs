using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゴールしたかを見るクラス
/// </summary>
public class InGameManager : SingletonMonoBehaviour<InGameManager>
{
    [Header("シーン情報を登録したスクリプタブルオブジェクト")]
    [SerializeField] private SceneReference sceneReference = null;

    [Header("Player")]
    [SerializeField] private Transform player = null;

    [Header("出口（Exit）")]
    [SerializeField] private Transform exit = null;

    /// <summary>
    /// プレイヤーが出口に到達したら発火するイベント
    /// </summary>
    public event Action<bool> OnExit;

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        if (!SwitchManager.Instance.IsPressed) return;

        if (DistanceUtil.IsNear(player, exit))
        {
            OnExit?.Invoke(true);
            Invoke(nameof(BackToTitle), 1.5f); ;
        }
    }

    private void BackToTitle()
    {
        SceneManager.LoadScene(sceneReference.TitleSceneName);
    }
}
