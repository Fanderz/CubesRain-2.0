using System.Collections;
using UnityEngine;

public class Cube : BaseObject
{
    [SerializeField] private Sphere _bombPrefab;

    private float _minSeconds = 2.0f;
    private float _maxSeconds = 5.0f;

    //private Renderer _renderer;
    private Coroutine _coroutine;

    private void Awake()
    {
        //_renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider is MeshCollider)
        {
            _coroutine = StartCoroutine(SpawnBomb());
        }
    }

    private IEnumerator SpawnBomb()
    {
        yield return new WaitForSeconds(Random.Range(_minSeconds, _maxSeconds));

        Deactivate();

        var bomb = Pool<Sphere>.OnGet(_bombPrefab);

        if (bomb != null)
            bomb.SetPosition(transform.position);

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }
}