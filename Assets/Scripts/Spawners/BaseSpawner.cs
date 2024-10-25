using UnityEngine;
using System;

public class BaseSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T Prefab;
    [SerializeField] protected Transform Parent;
    [SerializeField] protected int PoolMaxSize;

    protected int SpawnedObjectsCount;

    protected Pool<T> Pool;

    public virtual event Action<int> ChangedSpawnedCounter;
    public virtual event Action<int> ChangedCreatedCounter;
    public virtual event Action<int> ChangedActiveCounter;

    protected virtual void Awake()
    {
        Pool = new Pool<T>(PoolMaxSize, Prefab, transform);
        SpawnedObjectsCount = 0;
    }

    protected void FixedUpdate()
    {
        ChangedActiveCounter?.Invoke(Pool.ActiveCount);
    }

    protected T GetObject()
    {
        var obj = Pool.Get();

        if(obj != null)
        {
            SpawnedObjectsCount++;

            ChangedCreatedCounter?.Invoke(Pool.Count);
            ChangedSpawnedCounter?.Invoke(SpawnedObjectsCount);
        }

        return obj;
    }
}
