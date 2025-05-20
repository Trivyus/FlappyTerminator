using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action CollisionDetected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.TryGetComponent<Coin>(out _))
            CollisionDetected?.Invoke();
    }
}
