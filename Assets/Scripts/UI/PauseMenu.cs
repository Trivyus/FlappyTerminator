using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : BaseMenu
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private InputReader _inputReader;

    private bool _isPaused = false;

    private void OnEnable()
    {
        _inputReader.PauseButtonPressed += TogglePause;
    }

    private void OnDisable()
    {
        _inputReader.PauseButtonPressed -= TogglePause;
    }

    public void TogglePause()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            Time.timeScale = 0f;
            _pauseButton.enabled = false;
            _pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            _pauseButton.enabled = true;
            _pausePanel.SetActive(false);
        }
    }
}
