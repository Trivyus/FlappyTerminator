using System;
using UnityEngine;

public abstract class PoolableObject<T> : MonoBehaviour where T : PoolableObject<T>
{
    public event Action<T> Collided;

    public virtual void ResetState()
    {
        if (TryGetComponent(out Rigidbody2D rigidbody))
        {
            rigidbody.velocity = Vector2.zero;
            rigidbody.angularVelocity = 0f;
        }

        StopAllCoroutines();
        Collided = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collided?.Invoke((T)this);
    }
}