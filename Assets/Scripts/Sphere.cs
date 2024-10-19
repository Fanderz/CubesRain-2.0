
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Sphere : BaseObject
{
    [SerializeField] private float _explosionForce/* = 50f*/;
    [SerializeField] private float _explosionRadius/* = 10f*/;
    [SerializeField] private float _smoothTransparencyValue;

    //private float _explosionSeconds;

    private Coroutine _coroutine;

    private MeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        _coroutine = StartCoroutine(ChangeTransparency());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    public void SetPosition(Vector3 position) =>
        this.transform.position = position;

    private void Explode()
    {
        GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        Pool<Sphere>.OnRelease(this);
    }

    private IEnumerator ChangeTransparency()
    {
        while (_renderer.material.color.a != 0f)
        {
            _renderer.material.color = new Color(_renderer.material.color.r, _renderer.material.color.g, _renderer.material.color.b, Mathf.MoveTowards(_renderer.material.color.a, 0f, _smoothTransparencyValue));

            yield return null;
        }

        Explode();
    }
}
