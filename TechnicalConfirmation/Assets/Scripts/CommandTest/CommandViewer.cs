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
        text.text = $"<color=#4EC9B0>Position</color>\nx : {value.x}\ny : 0.0\nz : {value.y}";
    }
}
