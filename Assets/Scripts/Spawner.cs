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

    private bool isEnabled = false;

    private WaitForSeconds _wait;
    private Coroutine _cubesCoroutine;
    private Coroutine _bombsCoroutine;

    private Pool<Cube> _cubes;
    private Pool<Sphere> _spheres;

    private void Awake()
    {
        _wait = new WaitForSeconds(_spawnDelay);
        _cubes = new Pool<Cube>(_poolMaxSize);
        _spheres = new Pool<Sphere>(_poolMaxSize);
        _abyss.Destroying += _cubes.Destroy;
    }

    private void OnEnable()
    {
        isEnabled = true;
        _cubesCoroutine = StartCoroutine(SpawnCubesCoroutine());
    }

    private void OnDisable()
    {
        isEnabled = false;

        _abyss.Destroying -= _cubes.Destroy;
    }

    private void SpawnBomb(Cube cube)
    {
        _bombsCoroutine = StartCoroutine(SpawnBombCoroutine(cube));
    }

    private IEnumerator SpawnCubesCoroutine()
    {
        while (isEnabled)
        {
            Vector3 startPosition = new Vector3(Random.Range(-_xStartPosition, _xStartPosition),
                Random.Range(_yMinPosition, _yMaxPosition), Random.Range(-_zStartPosition, _zStartPosition));

            var cube = _cubes.Get(_cubePrefab, transform);

            if (cube != null)
            {
                cube.SpawningBomb -= SpawnBomb;
                cube.transform.position = startPosition;
                cube.SpawningBomb += SpawnBomb;
            }

            yield return _wait;
        }
    }

    private IEnumerator SpawnBombCoroutine(Cube cube)
    {
        yield return new WaitForSeconds(Random.Range(cube.MinSeconds, cube.MaxSeconds));

        _cubes.Release(cube);

        var bomb = _spheres.Get(_bombPrefab, transform);

        if (bomb != null)
        {
            bomb.Releasing -= _spheres.Release;

            if (cube != null)
                bomb.SetPosition(cube.transform.position);

            bomb.Releasing += _spheres.Release;
        }

        if (_bombsCoroutine != null)
            StopCoroutine(_bombsCoroutine);
    }
}