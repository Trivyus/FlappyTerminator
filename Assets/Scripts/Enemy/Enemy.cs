using UnityEngine;

[RequireComponent (typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Enemy : PoolableObject<Enemy> 
{
    [SerializeField] private Mover _mover;
    [SerializeField] private ShootingSystem _shootingSystem;

    private int _moveDirection = -1;

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
}
