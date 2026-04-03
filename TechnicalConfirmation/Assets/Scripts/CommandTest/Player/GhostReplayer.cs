using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゴーストプレイヤーの動きを再生するクラス
/// </summary>
public class GhostReplayer : MonoBehaviour
{
    private Simulate ghostSimulate;

    [Header("InputGetterの参照")]
    [SerializeField] private InputGetter inputGetter = null;

    [SerializeField] private PlayerViewer ghostPlayer;

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
        ghostSimulate = new Simulate();
        ghostPlayer.Initialized(ghostSimulate);
    }

    #region イベント登録、解除
    private void OnEnable()
    {
        inputGetter.OnReplayRequested += StartReplay;
    }

    private void OnDisable()
    {
        inputGetter.OnReplayRequested -= StartReplay;
    }
    #endregion

    /// <summary>
    /// GhostSimulateクラスをMoveCommandで動かすコルーチンを開始するメソッド
    /// </summary>
    public void StartReplay(List<Direction> directions)
    {
        StartCoroutine(ReplayRoutine(directions));
    }

    /// <summary>
    /// MoveCommandを順番に実行していくコルーチン
    /// </summary>
    private IEnumerator ReplayRoutine(List<Direction> directions)
    {
        foreach (Direction direction in directions)
        {
            ICommand command = new MoveCommand(ghostSimulate, direction);
            command.Execute();

            yield return new WaitForSeconds(0.3f);
        }
    }
}
