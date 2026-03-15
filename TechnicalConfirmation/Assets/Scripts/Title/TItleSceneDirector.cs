using UnityEngine;
using UnityEngine.InputSystem;

public class TItleSceneDirector : MonoBehaviour
{
    private int imageNumber = -1;

    void Start()
    {
        TitleSceneUIManager.Instance.BlinkingUI();

    }

    private void OnNavigate(InputValue inputValue)
    {
        var nowValue = inputValue.Get<Vector2>();
        if (nowValue.x > 0.0f)
        {
            imageNumber++;
        }
        else if (nowValue.x < 0.0f)
        {
            imageNumber--;
        }

        if (imageNumber < 0 || imageNumber > 6)
        {
            imageNumber = Mathf.Clamp(imageNumber, 0, 6);
            return;
        }

        TitleSceneUIManager.Instance.ImageChange(imageNumber);
    }

    void Update()
    {

    }
}
