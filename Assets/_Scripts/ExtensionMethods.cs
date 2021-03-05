using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{

    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }


    /// <summary>
    /// Get a random value from the list
    /// </summary>
    /// <typeparam name="T">type of object in the list</typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static T GetRandomFromList<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
}
