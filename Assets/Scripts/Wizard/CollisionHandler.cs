using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action Triggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<ICollisionTarget>(out _))
            Triggered?.Invoke();
    }
}
