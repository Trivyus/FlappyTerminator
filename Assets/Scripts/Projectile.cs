using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : PoolableObject<Projectile>
{
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetVelocity(Vector2 velocity)
    {
        _rigidbody.velocity = velocity;
    }
}
