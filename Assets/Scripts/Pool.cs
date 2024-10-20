using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour, new()
{
    private int _poolMaxSize;

    private List<T> _objects;

    public Pool(int maxSize)
    {
        _objects = new List<T>();
        _poolMaxSize = maxSize;
    }

    private T Create(T prefab, Transform parent = null)
    {
        return Object.Instantiate(prefab, parent);
    }

    public T Get(T prefab, Transform parent = null)
    {
        T result = null;

        if (_objects.FindAll(obj => obj.gameObject.activeSelf == false).Count > 0)
        {
            result = _objects.Find(obj => obj.gameObject.activeSelf == false);

            if (result != null)
            {
                result.gameObject.SetActive(true);
            }
        }
        else
        {
            if (_objects.Count < _poolMaxSize)
            {
                result = Create(prefab, parent);
                _objects.Add(result);
            }
        }

        return result;
    }

    public void Release(T obj)
    {
        var finded = _objects.Find(finded => finded.Equals(obj));

        if (finded != null)
            finded.gameObject.SetActive(false);
    }

    public void Destroy(T obj)
    {
        var finded = _objects.Find(finded => finded.Equals(obj));

        if (finded != null)
        {
            _objects.Remove(finded);

            Object.Destroy(finded.gameObject);
        }

    }
}
