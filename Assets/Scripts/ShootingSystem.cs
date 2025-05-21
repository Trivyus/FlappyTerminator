using System;
using System.Collections;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private float _projectileSpeed = 15f;
    [SerializeField] private float _verticalAngleModifier = 0f;
    [SerializeField] private float _maxVerticalAngle = 30f;
    [SerializeField] private float _projectileLifetime = 8f;

    private Pool<Projectile> _projectilePool;
    private float _nextFireTime;
    private Coroutine _coroutine;

    public event Action Shooted;

    public void SetProjectilePool(Pool<Projectile> projectilePool)
    {
        _projectilePool = projectilePool;
    }

    public void TryShoot(Vector2 direction)
    {
        if (Time.time >= _nextFireTime)
        {
            Shoot(direction.normalized);
            Shooted?.Invoke();
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
        projectile.SetDirection(direction, _projectileSpeed, _firePoint);
        _coroutine = StartCoroutine(DeactivateAfterLifetime(projectile));
        projectile.Triggered -= DeactivateAfterTriggered;
        projectile.Triggered += DeactivateAfterTriggered;
    }

    private IEnumerator DeactivateAfterLifetime(Projectile projectile)
    {
        yield return new WaitForSeconds(_projectileLifetime);

        if (projectile.gameObject.activeInHierarchy)
            _projectilePool.ReturnObject(projectile);
    }

    private void DeactivateAfterTriggered(Projectile projectile)
    {
        if (projectile != null && projectile.gameObject.activeInHierarchy)
        {
            StopWaitingDeactivation();
            _projectilePool.ReturnObject(projectile);
        }
    }

    private void StopWaitingDeactivation()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }
}