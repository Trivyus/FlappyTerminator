using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _minSpawnDelay = 2f;
    [SerializeField] private float _maxSpawnDelay = 5f;
    [SerializeField] private float _spawnDistanceFromCamera = 1.5f;
    [SerializeField] private float _minHeight = -10f;
    [SerializeField] private float _maxHeight = 10f;
    [SerializeField] private float _enemyLifetime = 15f;

    [SerializeField] private CoinsSpawner _coinsSpawner;
    [SerializeField] private Pool<Enemy> _enemyPool;
    [SerializeField] private Pool<Projectile> _projectilePool;

    private Coroutine _coroutine;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (enabled)
        {
            float delay = Random.Range(_minSpawnDelay, _maxSpawnDelay++);
            yield return new WaitForSeconds(delay);

            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Enemy enemy = _enemyPool.GetObject();
        enemy.SetProjectilePool(_projectilePool);
        enemy.gameObject.SetActive(true);
        enemy.transform.position = GetSpawnPosition();

        _coroutine = StartCoroutine(DeactivateAfterLifetime(enemy));
        enemy.Triggered -= DeactivateAfterTriggered;
        enemy.Triggered += DeactivateAfterTriggered;
    }

    private Vector3 GetSpawnPosition()
    {
        float cameraRightEdge = _mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        float spawnX = cameraRightEdge + _spawnDistanceFromCamera;
        float spawnY = Random.Range(_minHeight, _maxHeight);

        return new Vector3(spawnX, spawnY, 0);
    }

    private IEnumerator DeactivateAfterLifetime(Enemy enemy)
    {
        yield return new WaitForSeconds(_enemyLifetime);

        if (enemy.gameObject.activeInHierarchy)
            _enemyPool.ReturnObject(enemy);
    }

    private void DeactivateAfterTriggered(Enemy enemy)
    {
        if (enemy != null && enemy.gameObject.activeInHierarchy)
        {
            enemy.Triggered -= DeactivateAfterTriggered;
            StopWaitingDeactivation();
            _coinsSpawner.Drop(enemy.transform);
            _enemyPool.ReturnObject(enemy);
        }
    }

    private void StopWaitingDeactivation()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }
}