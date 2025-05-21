using System.Collections;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private CoinCollector _coinCollector;
    [SerializeField] private Pool<Coin> _coinsPool;
    [SerializeField] private float _coinLifetime = 15f;

    private Coroutine _coroutine;

    public void Drop(Transform enemyTransform)
    {
        Coin coin = _coinsPool.GetObject();
        coin.gameObject.SetActive(true);
        coin.transform.position = enemyTransform.position;
        _coroutine = StartCoroutine(DeactivateAfterLifetime(coin));
        _coinCollector.Triggered += DeactivateCoinAfterTriggered;
    }

    private IEnumerator DeactivateAfterLifetime(Coin coin)
    {
        yield return new WaitForSeconds(_coinLifetime);

        if (coin.gameObject.activeInHierarchy)
            _coinsPool.ReturnObject(coin);
    }

    private void DeactivateCoinAfterTriggered(Coin coin)
    {
        if (coin.gameObject.activeInHierarchy)
        {
            StopWaitingDeactivation();
            _coinCollector.Triggered -= DeactivateCoinAfterTriggered;
            _coinsPool.ReturnObject(coin);
        }
    }

    private void StopWaitingDeactivation()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }
}