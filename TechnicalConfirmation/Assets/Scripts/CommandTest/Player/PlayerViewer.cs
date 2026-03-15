using UnityEngine;

public class PlayerViewer : MonoBehaviour
{
    [SerializeField] private Transform nowPosition = null;

    public void Initialized(Simulate simulate)
    {
        simulate.OnPositionChanged += Position;
    }

    private void Position(Vector2 vector)
    {
        nowPosition.position = new Vector3(
            vector.x,
            nowPosition.position.y,
            vector.y
            );
    }
}
