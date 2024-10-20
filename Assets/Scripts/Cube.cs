using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public float MinSeconds { get; private set; }
    public float MaxSeconds { get; private set; }

    public event Action<Cube> SpawningBomb;

    private void Awake()
    {
        MinSeconds = 2.0f;
        MaxSeconds = 5.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider is MeshCollider)
        {
            SpawningBomb?.Invoke(this);
        }
    }
}