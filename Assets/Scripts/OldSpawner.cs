using UnityEngine;
using UnityEngine.Pool;

public class OldSpawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private float _repeatRate = 0.2f;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 10;

    private float _xStartPosition = 10f;
    private float _zStartPosition = 5f;
    private float _yMinPosition = 5f;
    private float _yMaxPosition = 10f;

    private ObjectPool<GameObject> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_prefab.gameObject),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void ActionOnGet(GameObject obj)
    {
        Vector3 startPosition = new Vector3(Random.Range(-_xStartPosition, _xStartPosition),
            Random.Range(_yMinPosition, _yMaxPosition), Random.Range(-_zStartPosition, _zStartPosition));
        obj.transform.position = startPosition;
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obj.SetActive(true);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _repeatRate);
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private void OnTriggerEnter(Collider other)
    {
        _pool.Release(other.gameObject);
    }
}
