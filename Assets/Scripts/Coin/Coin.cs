using System;
using UnityEngine;

public class Coin : PoolableObject<Coin>
{
    [SerializeField] private string _playerTag = "Player";

    public event Action<Coin> Trigered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(_playerTag))
        {
            Trigered?.Invoke(this);
        }
    }
}