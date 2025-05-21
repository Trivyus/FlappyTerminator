using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Enemy : PoolableObject<Enemy>
{
    [SerializeField] private Mover _mover;
    [SerializeField] private ShootingSystem _shootingSystem;

    private int _moveDirection = -1;

    public event Action<Enemy> Triggered;

    private void Update()
    {
        _mover.Move(_moveDirection);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Projectile>(out _) || other.TryGetComponent<Wizard>(out _))
            Triggered?.Invoke(this);
    }
}