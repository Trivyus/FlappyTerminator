using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action CollisionDetected;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionDetected?.Invoke();
    }
}
