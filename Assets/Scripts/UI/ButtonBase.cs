using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonBase : MonoBehaviour
{
    [SerializeField] private Button _button;

    protected virtual void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    protected virtual void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    protected abstract void OnButtonClicked();
}
