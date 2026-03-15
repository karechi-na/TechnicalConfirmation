using UnityEngine;
using TMPro;
using DG.Tweening;

public class StartUI : MonoBehaviour
{
    private const float DEFAULT_FADE_SECOND = 0.8f;

    [Header("“_–Å‚³‚¹‚½‚¢UI")]
    [SerializeField] private TextMeshProUGUI blinkingUI = null;

    public void BlinkLoop(float fadeSeconds = DEFAULT_FADE_SECOND)
    {
        blinkingUI.DOFade(0, fadeSeconds)
                  .SetEase(Ease.OutQuint)
                  .SetLoops(-1, LoopType.Yoyo);
    }
}
