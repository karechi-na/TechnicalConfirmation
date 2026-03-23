using System;
using UnityEngine;

public class InGameManager : SingletonMonoBehaviour<InGameManager>
{
    [SerializeField] private Transform player = null;

    [SerializeField] private Transform exit = null;

    public event Action<bool> OnExit;

    private void Update()
    {
        bool isPlayerOnExit = DistanceUtil.IsNear(player, exit);
        Debug.Log("‹——£ : " + Vector3.Distance(player.position, exit.position));
        if (isPlayerOnExit)
        {
            OnExit?.Invoke(true);
        }
    }
}
