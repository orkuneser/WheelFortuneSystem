using System;

public static class ArrayExtensions
{
    public static T[] ShuffledCopy<T>(this T[] source)
    {
        if (source == null || source.Length == 0)
            return Array.Empty<T>();

        var arr = (T[])source.Clone();
        for (int i = arr.Length - 1; i > 0; i--)
        {
            int r = UnityEngine.Random.Range(0, i + 1);
            (arr[i], arr[r]) = (arr[r], arr[i]);
        }
        return arr;
    }
}