using System.Collections.Generic;
using UnityEngine;

public class GridManager : SingletonMonoBehaviour<GridManager>
{
    [SerializeField] private float cellSize = 1.0f;
    [SerializeField] private Vector3 originPosition = Vector3.zero;
    [SerializeField] private float searchRadius = 0.45f;

    public Vector3 GridToWorldPosition(Vector2Int gridPosition, float yOffset = 0.0f)
    {
        return originPosition + new Vector3(gridPosition.x * cellSize, yOffset, gridPosition.y * cellSize);
    }

    public List<GridTileBase> GetTiles(Vector2Int gridPosition)
    {
        Vector3 worldPosition = GridToWorldPosition(gridPosition);
        Collider[] hits = Physics.OverlapSphere(worldPosition, searchRadius);

        List<GridTileBase> tiles = new List<GridTileBase>();

        foreach (var hit in hits)
        {
            GridTileBase tile = hit.GetComponent<GridTileBase>();
            if (tile != null)
            {
                tiles.Add(tile);
            }
        }

        return tiles;
    }

    public BoxTile GetBox(Vector2Int gridPosition)
    {
        Vector3 worldPosition = GridToWorldPosition(gridPosition);
        Collider[] hits = Physics.OverlapSphere(worldPosition, searchRadius);

        foreach (var hit in hits)
        {
            BoxTile box = hit.GetComponent<BoxTile>();
            if (box != null)
            {
                return box;
            }
        }
        return null;
    }

    public bool HasBox(Vector2Int gridPosition)
    {
        return GetBox(gridPosition) != null;
    }

    public bool CanBoxMoveTo(Vector2Int gridPosition)
    {
        if (!ExistsTile(gridPosition))
        {
            return false;
        }

        if (HasBox(gridPosition))
        {
            return false;
        }

        return CanEnter(gridPosition, PlayerType.Heavy);
    }

    public bool CanEnter(Vector2Int gridPosition, PlayerType playerType)
    {
        List<GridTileBase> tiles = GetTiles(gridPosition);

        if (tiles.Count == 0)
        {
            return false;
        }

        foreach (var tile in tiles)
        {
            if (!tile.CanEnter(playerType))
            {
                return false;
            }
        }
        return true;
    }

    public bool ExistsTile(Vector2Int gridPosition)
    {
        return GetTiles(gridPosition).Count > 0;
    }

    public bool IsFence(Vector2Int gridPosition)
    {
        List<GridTileBase> tiles = GetTiles(gridPosition);

        foreach (var tile in tiles)
        {
            if (tile is FenceTile)
            {
                return true;
            }
        }
        return false;
    }
}
