using UnityEngine;

public class WizardTracker : MonoBehaviour
{
    [SerializeField] private Wizard _wizard;
    [SerializeField] private float _xOffset;

    private void LateUpdate()
    {
        var position = transform.position;
        position.x = _wizard.transform.position.x + _xOffset;
        transform.position = position;
    }
}
