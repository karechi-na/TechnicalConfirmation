using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : DontDestroySingletonMonobehaviour<SceneLoader>
{
    // シーン名を格納するためのSceneReferenceクラスのインスタンス
    private SceneReference sceneReference = null;

    /// <summary>
    /// タイトルシーン以外からシーンがロードされたときにSceneLoaderを初期化するメソッド
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        CreateInstance();
    }

    protected override void Awake()
    {
        base.Awake();

        LoadData();
    }

    /// <summary>
    /// ResourcesフォルダからSceneReferenceを読み込むメソッド
    /// </summary>
    private void LoadData()
    {
        sceneReference = Resources.Load<SceneReference>(nameof(SceneReference));

        if (sceneReference == null)
        {
            Debug.LogError("SceneReference asset not found in Resources folder.");
        }
    }

    /// <summary>
    /// タイトルシーンに遷移するメソッド
    /// </summary>
    public void LoadTitleScene()
    {
        SceneManager.LoadScene(sceneReference.TitleSceneName);
    }

    /// <summary>
    /// シーン1に遷移するメソッド
    /// </summary>
    public void OnScene1Transition()
    {
        SceneManager.LoadScene(sceneReference.SceneName1);
    }

    /// <summary>
    /// シーン2に遷移するメソッド
    /// </summary>
    public void OnScene2Transition()
    {
        SceneManager.LoadScene(sceneReference.SceneName2);
    }

    /// <summary>
    /// シーン3に遷移するメソッド
    /// </summary>
    public void OnScene3Transition()
    { 
        SceneManager.LoadScene(sceneReference.SceneName3);
    }
}
