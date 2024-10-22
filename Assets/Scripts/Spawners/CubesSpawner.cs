using System;
using System.Collections;
using UnityEngine;

public class CubesSpawner : BaseSpawner<Cube>
{
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Abyss _abyss;
    [SerializeField] private BombsSpawner _bombSpawner;

    private float _xStartPosition = 7f;
    private float _zStartPosition = 5f;
    private float _yMinPosition = 8f;
    private float _yMaxPosition = 10f;

    private bool isEnabled = false;

    private WaitForSeconds _wait;
    private Coroutine _coroutine;
    private Pool<Cube> _pool;

    public override event Action<int> ChangedSpawnedCounter;
    public override event Action<int> ChangedCreatedCounter;
    public override event Action<int> ChangedActiveCounter;

    private void Awake()
    {
        _pool = new Pool<Cube>(poolMaxSize, prefab, transform);
        _wait = new WaitForSeconds(_spawnDelay);
        _abyss.Releasing += _pool.Release;
        _bombSpawner.ReleasingCube += _pool.Release;
    }

    private void OnEnable()
    {
        isEnabled = true;

        _coroutine = StartCoroutine(SpawnCoroutine());
    }

    private void OnDisable()
    {
        isEnabled = false;

        _abyss.Releasing -= _pool.Release;

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void FixedUpdate()
    {
        ChangedActiveCounter?.Invoke(_pool.ActiveCount);
    }

    private IEnumerator SpawnCoroutine()
    {
        while (isEnabled)
        {
            Vector3 startPosition = new Vector3(UnityEngine.Random.Range(-_xStartPosition, _xStartPosition),
                UnityEngine.Random.Range(_yMinPosition, _yMaxPosition), UnityEngine.Random.Range(-_zStartPosition, _zStartPosition));

            var cube = _pool.Get();

            if (cube != null)
            {
                cube.transform.position = startPosition;

                cube.SpawningBomb -= _bombSpawner.SpawnBomb;
                cube.SpawningBomb += _bombSpawner.SpawnBomb;

                cube.Releasing -= _pool.Release;
                cube.Releasing += _pool.Release;

                spawnedObjectsCount++;

                ChangedCreatedCounter?.Invoke(_pool.Count);
                ChangedSpawnedCounter?.Invoke(spawnedObjectsCount);
            }

            yield return _wait;
        }
    }
}
