using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gridを管理するクラス
/// </summary>
public class GridManager : SingletonMonoBehaviour<GridManager>
{
    [Header("Grid Settings")]
    [Header("グリッドのセルのサイズ")]
    [SerializeField] private float cellSize = 1.0f;

    [Header("原点")]
    [SerializeField] private Vector3 originPosition = Vector3.zero;

    [Header("タイルを検索する範囲")]
    [SerializeField] private float searchRadius = 0.45f;

    /// <summary>
    /// GridPositionをワールド座標に変換するメソッド
    /// </summary>
    public Vector3 GridToWorldPosition(Vector2Int gridPosition, float yOffset = 0.0f)
    {
        return originPosition + new Vector3(gridPosition.x * cellSize, yOffset, gridPosition.y * cellSize);
    }

    /// <summary>
    /// OverlapSphereを使用して、指定されたグリッド位置に存在するタイルを取得するメソッド
    /// </summary>
    public List<GridTileBase> GetTiles(Vector2Int gridPosition)
    {
        Vector3 worldPosition = GridToWorldPosition(gridPosition);
        Collider[] hits = Physics.OverlapSphere(worldPosition, searchRadius);

        List<GridTileBase> tiles = new List<GridTileBase>();

        // ヒットしたコライダーからGridTileBaseコンポーネントを取得し、リストに追加
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

    /// <summary>
    /// OverlapSphereを使用して、指定されたグリッド位置に存在するBoxTileを取得するメソッド
    /// </summary>
    public BoxTile GetBox(Vector2Int gridPosition)
    {
        Vector3 worldPosition = GridToWorldPosition(gridPosition);
        Collider[] hits = Physics.OverlapSphere(worldPosition, searchRadius);

        // ヒットしたコライダーからBoxTileコンポーネントを取得し、最初に見つかったものを返す
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

    /// <summary>
    /// 指定されたグリッド位置にBoxTileが存在するかどうかを判定するメソッド
    /// </summary>
    public bool HasBox(Vector2Int gridPosition)
    {
        return GetBox(gridPosition) != null;
    }

    /// <summary>
    /// 指定されたグリッド位置にBoxTileが移動できるかどうかを判定するメソッド
    /// </summary>
    public bool CanBoxMoveTo(Vector2Int gridPosition)
    {
        //FloorTileがない、もしくはBoxがある場合は移動できないと判断
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

    /// <summary>
    /// 指定されたグリッド位置にプレイヤーが入れるかどうかを判定するメソッド
    /// </summary>
    public bool CanEnter(Vector2Int gridPosition, PlayerType playerType)
    {
        // 指定されたグリッド位置に存在するタイルを取得
        List<GridTileBase> tiles = GetTiles(gridPosition);

        // タイルが存在しない場合は入れないと判断
        if (tiles.Count == 0)
        {
            return false;
        }

        // タイルの中に入れないものがある場合は入れないと判断
        foreach (var tile in tiles)
        {
            if (!tile.CanEnter(playerType))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 指定されたグリッド位置にタイルが存在するかどうかを判定するメソッド
    /// </summary>
    public bool ExistsTile(Vector2Int gridPosition)
    {
        return GetTiles(gridPosition).Count > 0;
    }

    /// <summary>
    /// 指定されたグリッド位置にフェンスタイルのタイルが存在するかどうかを判定するメソッド
    /// </summary>
    public bool IsFence(Vector2Int gridPosition)
    {
        // 指定されたグリッド位置に存在するタイルを取得
        List<GridTileBase> tiles = GetTiles(gridPosition);

        // タイルの中にFenceTileが存在するかどうかをチェック
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
