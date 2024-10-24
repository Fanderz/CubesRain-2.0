using System;
using System.Collections;
using UnityEngine;

public class BaseSpawner<T> : MonoBehaviour
{
    [SerializeField] protected T Prefab;
    [SerializeField] protected Transform Parent;
    [SerializeField] protected int PoolMaxSize;

    protected int SpawnedObjectsCount;

    public virtual event Action<int> ChangedSpawnedCounter;
    public virtual event Action<int> ChangedCreatedCounter;
    public virtual event Action<int> ChangedActiveCounter;

    private void Awake()
    {
        SpawnedObjectsCount = 0;
    }
}
