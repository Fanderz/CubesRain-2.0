using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private bool _hitted;
    private Rigidbody _rigidbody;
    private Renderer _renderer;

    public event Action<Cube> SpawningBomb;
    public event Action<Cube> Releasing;

    public float MinSeconds { get; private set; }
    public float MaxSeconds { get; private set; }

    private void Awake()
    {
        MinSeconds = 2.0f;
        MaxSeconds = 5.0f;

        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _hitted = false;
    }

    private void OnEnable()
    {
        _renderer.material.color = Color.white;
        _hitted = false;
        _rigidbody.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider is MeshCollider && _hitted == false)
        {
            _hitted = true;
            _renderer.material.color = Color.red;
            Invoke("OnRelease", UnityEngine.Random.Range(MinSeconds, MaxSeconds));
        }
    }

    private void OnRelease()
    {
        Releasing?.Invoke(this);
        SpawningBomb?.Invoke(this);
    }
}