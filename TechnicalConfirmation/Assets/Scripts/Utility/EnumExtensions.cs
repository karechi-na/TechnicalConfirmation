using UnityEngine;

public static class EnumExtensions
{
    public static T Next<T>(this T value) where T : System.Enum
    {
        var values = (T[])System.Enum.GetValues(typeof(T));
        int index = System.Array.IndexOf(values, value);
        return values[(index + 1)  % values.Length];
    }
    public static T Prev<T>(this T value) where T : System.Enum
    {
        var values = (T[])System.Enum.GetValues(typeof(T));
        int index = System.Array.IndexOf(values, value);
        return values[(index - 1)  % values.Length];
    }
}
