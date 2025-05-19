using System;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private float _projectileSpeed = 15f;

    private Pool<Projectile> _projectilePool;
    private float _nextFireTime;

    public event Action Shot;

    public void SetProjectilePool(Pool<Projectile> projectilePool)
    {
        _projectilePool = projectilePool;
    }

    public void TryShoot(Vector2 direction)
    {
        if (Time.time >= _nextFireTime)
        {
            Shoot(direction.normalized);
            Shot?.Invoke();
            _nextFireTime = Time.time + _fireRate;
        }
    }

    public void TryShoot()
    {
        TryShoot(_firePoint.right);
    }

    private void Shoot(Vector2 direction)
    {
        Projectile projectile = _projectilePool.GetObject();
        projectile.gameObject.SetActive(true);
        projectile.transform.position = _firePoint.position;
        projectile.transform.right = direction;
        projectile.SetVelocity(direction * _projectileSpeed);

        projectile.Collided -= DeactivateAfterCollision;
        projectile.Collided += DeactivateAfterCollision;
    }

    private void DeactivateAfterCollision(Projectile projectile)
    {
        if (projectile.gameObject.activeInHierarchy)
        {
            _projectilePool.ReturnObject(projectile);
        }
    }
}