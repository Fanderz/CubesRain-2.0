using System;

public class BombsSpawner : BaseSpawner<Sphere>
{
    private Pool<Sphere> _pool;

    public event Action<Cube> ReleasingCube;

    public override event Action<int> ChangedSpawnedCounter;
    public override event Action<int> ChangedCreatedCounter;
    public override event Action<int> ChangedActiveCounter;

    private void Awake()
    {
        _pool = new Pool<Sphere>(poolMaxSize, prefab, transform);
    }

    private void FixedUpdate()
    {
        ChangedActiveCounter?.Invoke(_pool.ActiveCount);
    }

    public void SpawnBomb(Cube cube)
    {
        if (cube != null)
        {
            var bomb = _pool.Get();

            if (bomb != null)
            {
                bomb.SetPosition(cube.transform.position);

                spawnedObjectsCount++;

                ChangedCreatedCounter?.Invoke(_pool.Count);
                ChangedSpawnedCounter?.Invoke(spawnedObjectsCount);

                bomb.Releasing += _pool.Release;
            }
        }
    }
}
