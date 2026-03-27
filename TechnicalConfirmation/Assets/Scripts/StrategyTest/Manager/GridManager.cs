using UnityEngine;

public class GridManager : SingletonMonoBehaviour<GridManager>
{
    [SerializeField] private float cellSize = 1.0f;
    [SerializeField] private Vector3 originPosition = Vector3.zero;

    public Vector3 GridToWorldPosition(Vector2Int gridPosition)
    {
        return originPosition + new Vector3(gridPosition.x * cellSize, 0.0f, gridPosition.y * cellSize);
    }

    public IGridTile GetTile(Vector2Int gridPosition)
    {
        Vector3 worldPos = GridToWorldPosition(gridPosition);
        Collider[] hits = Physics.OverlapSphere(worldPos, 0.4f);

        foreach (var hit in hits)
        {
            var tile = hit.GetComponent<IGridTile>();
            if(tile != null) return tile;
        }

        return null;
    }
}
