using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAdapter : MonoBehaviour
{
    private InputActions InputActions;
    private JoystickBehaviour Joystick { get; set; }

    [field: SerializeField]
    public Vector2 Position { get; set; }
    void Start()
    {
        InputActions = new InputActions();
        InputActions.Enable();

        Joystick = GetComponent<JoystickBehaviour>();
    }

    void Update()
    {
        var position = InputActions.Gameplay.Movement.ReadValue<Vector2>();
        Position = position;
        if (Input.GetMouseButton(0))
        {
            Joystick.Touch(position);
        }
        else
        {
            Joystick.Release();
        }
    }
}
