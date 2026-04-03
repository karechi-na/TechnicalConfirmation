using System;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; protected set; }
 
    protected virtual void Awake()
    {
        CheckInstance();
    }

    protected bool CheckInstance()
    {
        T self = this as T;

        if (Instance == null)
        {
            Instance = self;
            return true;
        }
        else if (Instance == self)
        {
            return true;
        }

            Debug.LogWarning($"An instance of {typeof(T).Name} already exists. Destroying the new one.");
            Destroy(gameObject);
            return false;
    }
}
