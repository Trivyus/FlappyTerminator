using System.Collections;
using UnityEngine;

public class EyeCombatLogic : MonoBehaviour
{
    [SerializeField] private float _minShootDelay = 1f;
    [SerializeField] private float _maxShootDelay = 3f;

    [SerializeField] private ShootingSystem _shootingSystem;

    private Coroutine _shootingCoroutine;

    private void Awake()
    {
        _shootingSystem = GetComponent<ShootingSystem>();
    }

    private void OnEnable()
    {
        StartShooting();
    }

    private void OnDisable()
    {
        StopShooting();
    }

    public void StartShooting()
    {
        if (_shootingCoroutine != null)
            StopCoroutine(_shootingCoroutine);

        _shootingCoroutine = StartCoroutine(ShootingRoutine());
    }

    public void StopShooting()
    {
        if (_shootingCoroutine != null)
        {
            StopCoroutine(_shootingCoroutine);
            _shootingCoroutine = null;
        }
    }

    private IEnumerator ShootingRoutine()
    {
        while (enabled)
        {
            float delay = Random.Range(_minShootDelay, _maxShootDelay);
            yield return new WaitForSeconds(delay);

            if (!isActiveAndEnabled)
                yield break;

            Vector2 direction = Vector2.left;
            _shootingSystem.TryShoot(direction);
        }
    }
}
