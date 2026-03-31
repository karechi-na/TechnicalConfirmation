using UnityEngine;

public class BoxTile : MonoBehaviour
{
    [Header("References")]

    [Header("Positioning")]
    [SerializeField] private float yOffset = 0.35f;

    [Header("Grid Settings")]
    [SerializeField] private Vector2Int startGridPosition = Vector2Int.zero;

    public Vector2Int CurrentGridPosition { get; private set; }

    private void Start()
    {
        Initialize(startGridPosition);
    }

    public void Initialize(Vector2Int gridPosition)
    {
        CurrentGridPosition = gridPosition;
        transform.position = GridManager.Instance.GridToWorldPosition(CurrentGridPosition, yOffset);
    }

    public void MoveTo(Vector2Int targetGridPosition)
    {
        CurrentGridPosition = targetGridPosition;
        transform.position = GridManager.Instance.GridToWorldPosition(CurrentGridPosition, yOffset);
    }
}
