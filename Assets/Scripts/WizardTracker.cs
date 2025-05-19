using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardTracker : MonoBehaviour
{
    [SerializeField] private Wizard _wizard;
    [SerializeField] private float _xOffset;

    private void Update()
    {
        var position = transform.position;
        position.x = _wizard.transform.position.x + _xOffset;
        transform.position = position;
    }
}
