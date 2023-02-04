using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public TankInputActions tankInputActions;
    private InputAction movementInput;
    private InputAction rotationInput;
    private InputAction lookVector;

    public float Movemement => movementInput.ReadValue<float>();
    public float Rotation => rotationInput.ReadValue<float>();
    public Vector2 Look => lookVector.ReadValue<Vector2>();

    private void Awake()
    {
        tankInputActions = new TankInputActions();
    }

    private void OnEnable()
    {
        movementInput = tankInputActions.Tank.Movement;
        movementInput.Enable();

        rotationInput = tankInputActions.Tank.Turn;
        rotationInput.Enable();

        lookVector = tankInputActions.Tank.Look;
        lookVector.Enable();

        tankInputActions.Tank.Shoot.Enable();
        tankInputActions.Tank.ToggleSettings.Enable();
    }

    private void OnDisable()
    {
        movementInput.Disable();
        rotationInput.Disable();
        lookVector.Disable();

        tankInputActions.Tank.Shoot.Disable();
        tankInputActions.Tank.ToggleSettings.Disable();
    }
}
