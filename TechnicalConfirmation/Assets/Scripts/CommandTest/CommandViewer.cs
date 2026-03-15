using UnityEngine;
using TMPro;

public class CommandViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    public void Initialized(Simulate simulate)
    {
        simulate.OnPositionChanged += TextChange;
    }

    private void TextChange(Vector2 value)
    {
        
        text.text = "CommandCount" + value.ToString();
    }
}
