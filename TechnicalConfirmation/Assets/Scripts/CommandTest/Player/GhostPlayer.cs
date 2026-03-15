using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostPlayer : MonoBehaviour
{
    [SerializeField] private Transform nowPosi = null;

    private Simulate simulate;

    public void Initialized(Simulate simulate)
    {
        this.simulate = simulate;
    }

    public void StartReplay(List<Direction> directions)
    {
        StartCoroutine(Replay(directions));
    }

    public IEnumerator Replay(List<Direction> directions)
    {
        foreach (var dir in directions)
        {
            simulate.Move(dir);

            yield return new WaitForSeconds(0.3f);
        }
    }
}
