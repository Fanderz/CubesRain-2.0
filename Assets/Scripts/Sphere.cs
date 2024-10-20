
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Sphere : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _smoothTransparencyValue;

    private float _minTransparency = 0.1f;

    private Coroutine _coroutine;
    private MeshRenderer _renderer;

    public event Action<Sphere> Releasing;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        _renderer.material.color = new Color(_renderer.material.color.r, _renderer.material.color.g, _renderer.material.color.b, 1f);
        _coroutine = StartCoroutine(ChangeTransparency());
    }

    public void SetPosition(Vector3 position) =>
        transform.position = position;

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }

        Releasing?.Invoke(this);
    }

    private IEnumerator ChangeTransparency()
    {
        while (_renderer.material.color.a != _minTransparency)
        {
            _renderer.material.color = new Color(_renderer.material.color.r, _renderer.material.color.g, _renderer.material.color.b, Mathf.MoveTowards(_renderer.material.color.a, _minTransparency, _smoothTransparencyValue));

            yield return null;
        }

        Explode();
    }
}
