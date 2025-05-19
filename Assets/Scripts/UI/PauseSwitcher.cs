using UnityEngine;

public class PauseSwitcher : ButtonBase
{
    [SerializeField] private PauseMenu _menu;

    protected override void OnButtonClicked()
    {
        _menu.TogglePause();
    }
}
