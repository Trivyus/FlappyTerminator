using UnityEngine;

public class RestartButton : ButtonBase
{
    [SerializeField] private PauseMenu _menu;

    protected override void OnButtonClicked()
    {
        _menu.RestartGame();
    }
}
