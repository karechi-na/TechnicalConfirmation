using UnityEngine;

public static class LogDisplay
{
    public static void LogDisp()
    {
        Debug.Log("ok!!!!");
    }

    public static void WarningDisp()
    {
        Debug.LogWarning("気を付けて！");
    }

    public static void ErrorDisp()
    {
        Debug.LogError("エラー出てるよ");
    }
}
