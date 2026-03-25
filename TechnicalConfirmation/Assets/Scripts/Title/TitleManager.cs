using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 遷移先を管理
/// </summary>
public class TitleManager : MonoBehaviour
{
    [Header("シーン名をまとめたスクリプタブルオブジェクト")]
    [SerializeField] private SceneReference sceneReference = null;

    /// <summary>
    /// SceneReferenceのSceneName1に登録されているシーンに遷移
    /// </summary>
    public void OnScene1Translate()
    {
        SceneManager.LoadScene(sceneReference.SceneName1);
    }

    /// <summary>
    /// SceneReferenceのSceneName2に登録されているシーンに遷移
    /// </summary>
    public void OnScene2Translate()
    {
        SceneManager.LoadScene(sceneReference.SceneName2);
    }

    /// <summary>
    /// SceneReferenceのSceneName3に登録されているシーンに遷移
    /// </summary>
    public void OnScene3Translate()
    {
        SceneManager.LoadScene(sceneReference.SceneName3);
    }
}
