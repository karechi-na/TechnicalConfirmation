using UnityEngine;
using TMPro;

public class GoalText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goalText = null;

    private void Start()
    {
        goalText.enabled = false;
    }

    private void OnEnable()
    {
        InGameManager.Instance.OnExit += TextChange;
        Debug.Log("イベント登録！");
    }
    private void OnDisable()
    {
        InGameManager.Instance.OnExit -= TextChange;
    }

    private void TextChange(bool isGoal)
    {
        Debug.Log("isGoal : " + isGoal);

        if (isGoal) goalText.enabled = true;
        else goalText.enabled = false;
    }
}
