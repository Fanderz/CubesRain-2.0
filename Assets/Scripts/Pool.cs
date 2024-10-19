using System.Collections.Generic;
using UnityEngine;

public static class Pool<T> where T : MonoBehaviour, new()
{
    public static int _poolMaxSize;

    private static readonly List<T> _poolActive = new List<T>();

    private static T OnCreate(T prefab, Transform parent = null)
    {
        var newObject = Object.Instantiate(prefab, parent);

        return newObject;
    }

    public static T OnGet(T prefab, Transform parent = null)
    {
        T result = null;

        if (_poolActive.FindAll(obj => obj.gameObject.activeSelf == false).Count > 0)
        {
            result = _poolActive.Find(obj => obj.gameObject.activeSelf == false);

            if (result != null)
            {
                result.gameObject.SetActive(true);
            }
        }
        else
        {
            if (_poolActive.Count < _poolMaxSize)
            {
                result = OnCreate(prefab);
                _poolActive.Add(result);
            }
        }

        return result;
    }

    public static void OnRelease(T obj)
    {
        var finded = _poolActive.Find(finded => finded.Equals(obj));
        finded.gameObject.SetActive(false);
    }

    public static void OnDestroy(T obj)
    {
        var finded = _poolActive.Find(finded => finded.Equals(obj));

        if (finded != null)
            _poolActive.Remove(finded);
    }
}
