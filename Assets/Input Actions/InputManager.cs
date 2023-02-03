using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public TankInputActions tankInputActions;
    private InputAction movementInput;
    private InputAction rotationInput;
    private InputAction lookVector;

    public float Movemement => movementInput.ReadValue<float>();
    public float Rotation => rotationInput.ReadValue<float>();
    public Vector2 Look => lookVector.ReadValue<Vector2>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning($"There should only be one instance of {Instance.GetType()}", this);
            Destroy(this);
        }

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
    }

    private void OnDisable()
    {
        movementInput.Disable();
        rotationInput.Disable();
        lookVector.Disable();

        tankInputActions.Tank.Shoot.Enable();
    }
}
