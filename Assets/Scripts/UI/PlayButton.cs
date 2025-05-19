using UnityEngine;

public class PlayButton : ButtonBase
{
    [SerializeField] private MainMenu _menu;
    protected override void OnButtonClicked()
    {
        _menu.PlayGame();
    }
}
