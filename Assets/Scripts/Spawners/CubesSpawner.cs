using UnityEngine;
using System.Collections;

public class CubesSpawner : BaseSpawner<Cube>
{
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Abyss _abyss;
    [SerializeField] private BombsSpawner _bombSpawner;

    private float _xStartPosition = 7f;
    private float _zStartPosition = 5f;
    private float _yMinPosition = 8f;
    private float _yMaxPosition = 10f;

    private WaitForSeconds _wait;
    private Coroutine _coroutine;

    protected override void Awake()
    {
        base.Awake();
        _wait = new WaitForSeconds(_spawnDelay);
    }

    private void OnEnable()
    {
        _abyss.Releasing += Pool.Release;
        _coroutine = StartCoroutine(SpawnCoroutine());
    }

    private void OnDisable()
    {
        _abyss.Releasing -= Pool.Release;

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    protected override void Spawn(Cube cube)
    {
        Vector3 startPosition = new Vector3(Random.Range(-_xStartPosition, _xStartPosition),
            Random.Range(_yMinPosition, _yMaxPosition), Random.Range(-_zStartPosition, _zStartPosition));

        cube.transform.position = startPosition;
        cube.Releasing += Release;
        cube.SpawningBomb += _bombSpawner.SpawnBomb;

        base.Spawn(cube);
    }

    protected override void Release(Cube cube)
    {
        cube.Releasing -= Release;

        base.Release(cube);
    }

    private IEnumerator SpawnCoroutine()
    {
        while (enabled)
        {
            var cube = GetObject();

            if (cube != null)
                Spawn(cube);

            yield return _wait;
        }
    }
}
