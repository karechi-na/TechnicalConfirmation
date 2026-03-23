using UnityEngine;

/// <summary>
/// 対象との距離を測るときに使うユーティリティクラス
/// </summary>
public static class DistanceUtil
{
    /// <summary>
    /// 距離を測り範囲内かを見る
    /// 返り値はbool
    /// </summary>
    public static bool IsNear(Transform a, Transform b, float distance = 0.5f)
    {
        return Vector3.Distance(a.position, b.position) <= distance;
    }
}
