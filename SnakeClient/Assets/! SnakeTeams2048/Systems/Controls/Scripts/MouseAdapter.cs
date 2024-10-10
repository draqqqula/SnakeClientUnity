using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAdapter : MonoBehaviour
{
    private InputActions _inputActions;
    private JoystickBehaviour _joystick;

    void Start()
    {
        _inputActions = new InputActions();
        _inputActions.Enable();

        _joystick = GetComponent<JoystickBehaviour>();
    }

    void Update()
    {
        var position = _inputActions.Gameplay.Movement.ReadValue<Vector2>();
        if (Input.GetMouseButton(0))
        {
            _joystick.Hold(position);
        }
        else
        {
            _joystick.Release();
        }
    }
}
