using System;
using System.Collections;
using UnityEngine;

public class BaseSpawner<T> : MonoBehaviour
{
    [SerializeField] protected T prefab;
    [SerializeField] protected Transform parent;
    [SerializeField] protected int poolMaxSize;

    protected int spawnedObjectsCount;

    public virtual event Action<int> ChangedSpawnedCounter;
    public virtual event Action<int> ChangedCreatedCounter;
    public virtual event Action<int> ChangedActiveCounter;

    private void Awake()
    {
        spawnedObjectsCount = 0;
    }
}
