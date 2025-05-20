using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Wizard : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private CharacterAnimator _characterAnimator;
    [SerializeField] private ShootingSystem _shootingSystem;
    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private DeathMenu _deathMenu;
    [SerializeField] private CoinCollector _coinCollector;
    [SerializeField] private Pool<Projectile> _projectilePool;

    private Rigidbody2D _rigidbody;
    private bool _isRising;
    private bool _isDead;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _shootingSystem.SetProjectilePool(_projectilePool);
        _inputReader.JumpButtonPressed += _mover.Jump;
        _inputReader.AttackButtonPressed += _shootingSystem.TryShoot;
        _shootingSystem.Shot += _characterAnimator.TriggerAtack;
        _collisionHandler.CollisionDetected += HandleDeath;
        _characterAnimator.DeathAnimationComleted += ShowGameOverMenu;
    }

    private void OnDisable()
    {
        _inputReader.JumpButtonPressed -= _mover.Jump;
        _inputReader.AttackButtonPressed -= _shootingSystem.TryShoot;
        _shootingSystem.Shot -= _characterAnimator.TriggerAtack;
        _collisionHandler.CollisionDetected -= HandleDeath;
        _characterAnimator.DeathAnimationComleted -= ShowGameOverMenu;
    }

    private void Update()
    {
        _mover.Move();
        DefineDirectionMovementY();
    }

    private void DefineDirectionMovementY()
    {
        float yVelocity = _rigidbody.velocity.y;

        if (yVelocity > 0.1f && !_isRising)
        {
            _isRising = true;
            _shootingSystem.SetVerticalAngleModifier(1f);
            _characterAnimator.UpdateJumping(_isRising);
            _characterAnimator.UpdateFalling(!_isRising);
        }
        else if (yVelocity < -0.1f && _isRising)
        {
            _isRising = false;
            _shootingSystem.SetVerticalAngleModifier(-1f);
            _characterAnimator.UpdateJumping(_isRising);
            _characterAnimator.UpdateFalling(!_isRising);
        }
    }

    private void HandleDeath()
    {
        if (_isDead) return;

        _isDead = true;

        _mover.StopMoving();
        _shootingSystem.enabled = false;
        _inputReader.enabled = false;
        _characterAnimator.TriggerDeath();
    }

    private void ShowGameOverMenu()
    {
        _deathMenu.ShowPanel(_coinCollector.CurrentScore);
        _coinCollector.ResetScore();
    }
}