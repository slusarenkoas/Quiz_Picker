using System.Collections.Generic;
using UnityEngine;

public static class RandomExtensions
{
    public static T GetRandomElements<T>(this IList<T> list)
    {
        var randomIndex = Random.Range(0, list.Count);
        return list[randomIndex];
    }
}