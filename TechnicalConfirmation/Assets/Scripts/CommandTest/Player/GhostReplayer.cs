using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostReplayer : MonoBehaviour
{
    private Simulate ghostSimulate;

    [SerializeField] private PlayerViewer ghostPlayer;

    private void Start()
    {
        ghostSimulate = new Simulate();
        ghostPlayer.Initialized(ghostSimulate);
    }

    #region ƒCƒxƒ“ƒg“o˜^
    private void OnEnable()
    {
        InputGetter.Instance.OnReplayRequested += StartReplay;
    }

    private void OnDisable()
    {
        InputGetter.Instance.OnReplayRequested -= StartReplay;
    }
    #endregion

    public void StartReplay(List<Direction> directions)
    {
        StartCoroutine(ReplayRoutine(directions));
    }

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
