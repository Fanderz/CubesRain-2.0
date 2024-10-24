using System;

public class BombsSpawner : BaseSpawner<Bomb>
{
    private Pool<Bomb> _pool;

    public event Action<Cube> ReleasingCube;

    public override event Action<int> ChangedSpawnedCounter;
    public override event Action<int> ChangedCreatedCounter;
    public override event Action<int> ChangedActiveCounter;

    private void Awake()
    {
        _pool = new Pool<Bomb>(PoolMaxSize, Prefab, transform);
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

                SpawnedObjectsCount++;

                ChangedCreatedCounter?.Invoke(_pool.Count);
                ChangedSpawnedCounter?.Invoke(SpawnedObjectsCount);

                bomb.Releasing -= _pool.Release;
                bomb.Releasing += _pool.Release;
            }
        }
    }
}
