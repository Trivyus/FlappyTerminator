using System;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private ScoreDisplay _scoreDisplay;

    public event Action<Coin> Triggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            Triggered?.Invoke(coin);
            _scoreDisplay.IncreaseScore();
        }
    }
}
