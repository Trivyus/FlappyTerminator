using System;
using UnityEngine;

public abstract class PoolableObject<T> : MonoBehaviour where T : PoolableObject<T>
{
    public virtual void ResetState()
    {
        if (TryGetComponent(out Rigidbody2D rigidbody))
        {
            rigidbody.velocity = Vector2.zero;
            rigidbody.angularVelocity = 0f;
        }
    }
}