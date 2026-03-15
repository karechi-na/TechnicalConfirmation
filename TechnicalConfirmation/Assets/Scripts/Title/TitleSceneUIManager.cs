using TitleScene.UI;
using UnityEngine;

public class TitleSceneUIManager : SingletonMonoBehaviour<TitleSceneUIManager>
{
    [Header("点滅させるUIにアタッチしているクラス")]
    [SerializeField] private StartUI startUI = null;

    [SerializeField] private No21Images no21 = null;

    public void BlinkingUI()
    {
        startUI.BlinkLoop();
    }

    public void ImageChange(int imageNumber)
    {
        no21.ImageChange(imageNumber);
    }
}
