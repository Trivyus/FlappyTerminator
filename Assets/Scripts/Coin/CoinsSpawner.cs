using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Pool<Coin> _coinsPool;

    public void Drop(Transform enemyTransform)
    {
        Coin coin = _coinsPool.GetObject();
        coin.gameObject.SetActive(true);
        coin.transform.position = enemyTransform.position;
        coin.Trigered += DeactivateCoinAfterTriggered;
    }

    private void DeactivateCoinAfterTriggered(Coin coin)
    {
        if (coin.gameObject.activeInHierarchy)
        {
            coin.Trigered -= DeactivateCoinAfterTriggered;
            _coinsPool.ReturnObject(coin);
        }
    }
}
