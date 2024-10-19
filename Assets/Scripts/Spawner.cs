using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private float _spawnDelay;

    private float _xStartPosition = 7f;
    private float _zStartPosition = 5f;
    private float _yMinPosition = 8f;
    private float _yMaxPosition = 10f;

    private bool isEnabled = false;

    private WaitForSeconds _wait;
    private Coroutine _coroutine;

    private void Awake()
    {
        _wait = new WaitForSeconds(_spawnDelay);
        Pool<Cube>._poolMaxSize = _poolMaxSize;
        Pool<Sphere>._poolMaxSize = _poolMaxSize;
    }

    private void OnEnable()
    {
        isEnabled = true;
        _coroutine = StartCoroutine(SpawnCubes());
    }

    private void OnDisable()
    {
        isEnabled = false;
        StopCoroutine(_coroutine);
    }

    private IEnumerator SpawnCubes()
    {
        while (isEnabled)
        {
            Vector3 startPosition = new Vector3(Random.Range(-_xStartPosition, _xStartPosition),
                Random.Range(_yMinPosition, _yMaxPosition), Random.Range(-_zStartPosition, _zStartPosition));

            var cube = Pool<Cube>.OnGet(_prefab, this.transform);

            if (cube != null)
                cube.transform.position = startPosition;

            yield return _wait;
        }
    }
}