using System;
using UnityEngine;

public class Abyss : MonoBehaviour
{
    public event Action<Cube> Releasing;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider is BoxCollider)
            if (collision.gameObject.TryGetComponent(out Cube cube))
                Releasing?.Invoke(cube);
    }
}
