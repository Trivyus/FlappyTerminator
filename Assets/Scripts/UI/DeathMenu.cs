using TMPro;
using UnityEngine;

public class DeathMenu : BaseMenu
{
    [SerializeField] private GameObject _deathPanel;
    [SerializeField] private TMP_Text _scoreText;

    public void ShowPanel(int curentScore)
    {
        Time.timeScale = 0f;
        _scoreText.text = $"Your Score: {curentScore}";
        _deathPanel.SetActive(true);
    }
}