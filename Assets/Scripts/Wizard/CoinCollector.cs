using TMPro;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private const string CoinsKey = "PlayerCoins";

    [SerializeField] private string _coinTag = "Coin";
    [SerializeField] private TMP_Text _scoreText;
    
    public int CurrentScore {  get; private set; }

    private void Start()
    {
        CurrentScore = PlayerPrefs.GetInt(CoinsKey, 0);
        ShowScore();
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        ShowScore();
        PlayerPrefs.SetInt(CoinsKey, CurrentScore);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(_coinTag))
        {
            UpdateScore();
        }
    }

    private void UpdateScore()
    {
        CurrentScore++;
        ShowScore();
        PlayerPrefs.SetInt(CoinsKey, CurrentScore);
    }

    private void ShowScore()
    {
        _scoreText.text = $"{CurrentScore}";
    }
}
