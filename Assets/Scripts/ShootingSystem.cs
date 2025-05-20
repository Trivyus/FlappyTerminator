using System;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private float _projectileSpeed = 15f;
    [SerializeField] private float _verticalAngleModifier = 0f;
    [SerializeField] private float _maxVerticalAngle = 30f;

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

    public void SetVerticalAngleModifier(float modifier)
    {
        _verticalAngleModifier = Mathf.Clamp(modifier, -1f, 1f) * _maxVerticalAngle;
    }

    private void Shoot(Vector2 direction)
    {
        if (_verticalAngleModifier != 0f)
        {
            direction = Quaternion.Euler(0, 0, _verticalAngleModifier) * direction;
        }

        Projectile projectile = _projectilePool.GetObject();
        projectile.gameObject.SetActive(true);
        projectile.transform.position = _firePoint.position;
        projectile.transform.right = direction;
        projectile.SetDirection(direction, _projectileSpeed);
        projectile.Triggered -= DeactivateAfterTriggered;
        projectile.Triggered += DeactivateAfterTriggered;
    }

    private void DeactivateAfterTriggered(Projectile projectile)
    {
        if (projectile.gameObject.activeInHierarchy)
        {
            _projectilePool.ReturnObject(projectile);
        }
    }
}