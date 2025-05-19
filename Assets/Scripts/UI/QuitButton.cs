using UnityEngine;

public class QuitButton : ButtonBase
{
    [SerializeField] private MainMenu _menu;
    protected override void OnButtonClicked()
    {
        _menu.QuitGame();
    }
}
