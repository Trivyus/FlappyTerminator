using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    private const string CoinsKey = "PlayerCoins";

    [SerializeField] private TMP_Text _scoreText;

    public int CurrentScore { get; private set; }

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

    public void IncreaseScore()
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
