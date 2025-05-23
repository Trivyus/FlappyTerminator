using System;
using UnityEngine;

public class Projectile : PoolableObject<Projectile>, ICollisionTarget
{
    [SerializeField] private TargetType _targetType;

    private Vector2 _direction;
    private float _speed;

    public event Action<Projectile> Triggered;

    private void Update()
    {
        transform.position += (Vector3)(_speed * Time.deltaTime * _direction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isTarget = false;

        switch (_targetType)
        {
            case TargetType.Enemy:
                isTarget = other.TryGetComponent<Enemy>(out _);
                break;
            case TargetType.Wizard:
                isTarget = other.TryGetComponent<Wizard>(out _);
                break;
        }

        if (isTarget)
            Triggered?.Invoke(this);
    }

    public void SetDirection(Vector2 direction, float speed, Transform firePoint)
    {
        _speed = speed;
        _direction = direction.normalized;
        transform.position = firePoint.position;
        transform.right = _direction;
    }
}