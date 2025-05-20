using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Enemy : PoolableObject<Enemy>
{
    [SerializeField] private Mover _mover;
    [SerializeField] private ShootingSystem _shootingSystem;

    private int _moveDirection = -1;

    public event Action<Enemy> Triggered;

    private void OnEnable()
    {
        if (_shootingSystem != null)
            _shootingSystem.enabled = true;
    }

    private void OnDisable()
    {
        if (_shootingSystem != null)
            _shootingSystem.SetProjectilePool(null);
    }

    private void Update()
    {
        _mover.Move(_moveDirection);
    }

    public override void ResetState()
    {
        base.ResetState();

        if (_shootingSystem != null)
        {
            _shootingSystem.SetProjectilePool(null);
            _shootingSystem.enabled = false;
        }
    }

    public void SetProjectilePool(Pool<Projectile> projectilePool)
    {
        if (_shootingSystem != null)
            _shootingSystem.SetProjectilePool(projectilePool);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Projectile>(out _) || other.TryGetComponent<Wizard>(out _))
            Triggered?.Invoke(this);
    }
}