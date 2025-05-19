using UnityEngine;

public class BackMenuButton : ButtonBase
{
    [SerializeField] private PauseMenu _menu;

    protected override void OnButtonClicked()
    {
        _menu.QuitToMainMenu();
    }
}
