using UnityEngine;

/// <summary>
/// DontDestroyOnLoadを使用したSingletonMonobehaviourを実装するためのクラス
/// </summary>
public abstract class DontDestroySingletonMonobehaviour<T> : SingletonMonoBehaviour<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if (!CheckInstance()) return;

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// シングルトンインスタンスを作成するためのメソッド。既にインスタンスが存在する場合は何もしない。
    /// </summary>
    protected static void CreateInstance()
    {
        if(Instance != null) return;

        GameObject obj = new GameObject(typeof(T).Name);
        obj.AddComponent<T>();
    }
}
