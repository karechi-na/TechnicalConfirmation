using UnityEngine;

/// <summary>
/// 入力に応じてシミュレートクラスのMoveメソッドに渡す方向を表す列挙型
/// </summary>
public enum Direction
{
    None = 0,
    [Tooltip("上↑")]Up,
    [Tooltip("下↓")] Down,
    [Tooltip("左←")] Left,
    [Tooltip("右→")] Right
}
