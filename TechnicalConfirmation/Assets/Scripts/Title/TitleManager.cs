using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// タイトルで行う処理を管理
/// </summary>
public class TitleManager : MonoBehaviour
{
    [Header("シーン名をまとめたスクリプタブルオブジェクト")]
    [SerializeField] private SceneReference sceneReference = null;

    [Header("エラーイメージ")]
    [SerializeField] private GameObject errorImage = null;

    /// <summary>
    /// SceneReferenceのSceneName1に登録されているシーンに遷移
    /// </summary>
    public void OnScene1Transition()
    {
        if (SceneNullCheck(sceneReference.SceneName1)) return;
        SceneLoader.Instance.OnScene1Transition();
    }

    /// <summary>
    /// SceneReferenceのSceneName2に登録されているシーンに遷移
    /// </summary>
    public void OnScene2Transition()
    {
        if (SceneNullCheck(sceneReference.SceneName2)) return;
        SceneLoader.Instance.OnScene2Transition();
    }

    /// <summary>
    /// SceneReferenceのSceneName3に登録されているシーンに遷移
    /// </summary>
    public void OnScene3Transition()
    {
        if (SceneNullCheck(sceneReference.SceneName3)) return;
        SceneLoader.Instance.OnScene3Transition();
    }

    /// <summary>
    /// SceneReferenceに登録されているシーン名がnullまたは空文字列かどうかをチェックするメソッド
    /// </summary>
    private bool SceneNullCheck(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            StartCoroutine(ShowErrorImage());
            return true;
        }
        return false;
    }

    private IEnumerator ShowErrorImage(float hideDelay = 3.5f)
    {
        errorImage.SetActive(true);
        yield return new WaitForSeconds(hideDelay);
        errorImage.SetActive(false);
    }
}
