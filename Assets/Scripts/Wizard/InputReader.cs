using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string JumpButton = "Jump";
    private const string AttackButton = "Fire1";

    private KeyCode _pauseKey = KeyCode.Escape;

    public event Action JumpButtonPressed;
    public event Action AttackButtonPressed;
    public event Action PauseButtonPressed;

    private void Update()
    {
        if (Input.GetButton(JumpButton))
            JumpButtonPressed?.Invoke();

        if (Input.GetButton(AttackButton))
            AttackButtonPressed?.Invoke();

        if (Input.GetKeyDown(_pauseKey))
            PauseButtonPressed?.Invoke();
    }
}
