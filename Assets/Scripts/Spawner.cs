using System;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Sphere _bombPrefab;
    [SerializeField] private Abyss _abyss;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private float _spawnDelay;

    private float _xStartPosition = 7f;
    private float _zStartPosition = 5f;
    private float _yMinPosition = 8f;
    private float _yMaxPosition = 10f;

    private int _spawnedCubesCount;
    private int _spawnedBombsCount;

    private bool isEnabled = false;

    private WaitForSeconds _wait;
    private Coroutine _cubesCoroutine;
    private Coroutine _bombsCoroutine;

    private Pool<Cube> _cubesPool;
    private Pool<Sphere> _bombsPool;

    public event Action<int> ChangedSpawnedCubesCounter;
    public event Action<int> ChangedSpawnedBombsCounter;

    public event Action<int> ChangedCreatedCubesCounter;
    public event Action<int> ChangedCreatedBombsCounter;

    public event Action<int> ChangedActiveCubesCounter;
    public event Action<int> ChangedActiveBombsCounter;

    private void Awake()
    {
        _wait = new WaitForSeconds(_spawnDelay);
        _cubesPool = new Pool<Cube>(_poolMaxSize);
        _bombsPool = new Pool<Sphere>(_poolMaxSize);
        _abyss.Destroying += _cubesPool.Destroy;

        _spawnedCubesCount = 0;
        _spawnedBombsCount = 0;
    }

    private void OnEnable()
    {
        isEnabled = true;
        _cubesCoroutine = StartCoroutine(SpawnCubesCoroutine());
    }

    private void OnDisable()
    {
        isEnabled = false;

        _abyss.Destroying -= _cubesPool.Destroy;
    }

    private void FixedUpdate()
    {
        ChangedActiveCubesCounter?.Invoke(_cubesPool.ActiveCount);
        ChangedActiveBombsCounter?.Invoke(_bombsPool.ActiveCount);
    }

    public void SpawnBomb(Cube cube)
    {
        _bombsCoroutine = StartCoroutine(SpawnBombCoroutine(cube));
    }

    private IEnumerator SpawnCubesCoroutine()
    {
        while (isEnabled)
        {
            Vector3 startPosition = new Vector3(UnityEngine.Random.Range(-_xStartPosition, _xStartPosition),
                UnityEngine.Random.Range(_yMinPosition, _yMaxPosition), UnityEngine.Random.Range(-_zStartPosition, _zStartPosition));

            var cube = _cubesPool.Get(_cubePrefab, transform);

            if (cube != null)
            {
                cube.transform.position = startPosition;
                cube.SpawningBomb += SpawnBomb;

                _spawnedCubesCount++;

                ChangedCreatedCubesCounter?.Invoke(_cubesPool.Count);
                ChangedSpawnedCubesCounter?.Invoke(_spawnedCubesCount);
            }

            yield return _wait;
        }
    }

    private IEnumerator SpawnBombCoroutine(Cube cube)
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(cube.MinSeconds, cube.MaxSeconds));

        _cubesPool.Release(cube);
        cube.SpawningBomb -= SpawnBomb;

        var bomb = _bombsPool.Get(_bombPrefab, transform);

        if (bomb != null)
        {
            bomb.Releasing -= _bombsPool.Release;

            if (cube != null)
            {
                bomb.SetPosition(cube.transform.position);

                _spawnedBombsCount++;

                ChangedCreatedBombsCounter?.Invoke(_bombsPool.Count);
                ChangedSpawnedBombsCounter?.Invoke(_spawnedBombsCount);
            }

            bomb.Releasing += _bombsPool.Release;
        }

        if (_bombsCoroutine != null)
            StopCoroutine(_bombsCoroutine);
    }
}