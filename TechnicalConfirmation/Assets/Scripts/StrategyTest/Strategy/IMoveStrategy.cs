using UnityEngine;

/// <summary>
/// Strategyパターンの基にするインターフェース
/// </summary>
public interface IMoveStrategy
{
    MoveResult TryMove(
        Vector2Int currentPosition,
        Vector2Int moveDirection,
        GridManager gridManager);
}
