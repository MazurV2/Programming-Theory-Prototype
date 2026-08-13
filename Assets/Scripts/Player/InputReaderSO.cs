using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReaderSO", menuName = "Input/Input Reader")]
public class InputReaderSO : ScriptableObject
{
    private Controls controls;

    public event Action<Vector2> onMoveInput;
    public event Action<bool> onShootInput;

    private void OnEnable()
    {
        controls = new Controls();
        controls.Enable();

        controls.Player.Move.performed += Move;
        controls.Player.Move.canceled += Move;

        controls.Player.Shoot.performed += Shoot;
        controls.Player.Shoot.canceled += Shoot;
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= Move;
        controls.Player.Move.canceled -= Move;

        controls.Player.Shoot.performed -= Shoot;
        controls.Player.Shoot.canceled -= Shoot;

        controls.Disable();
    }

    private void Move(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        onMoveInput?.Invoke(moveInput);
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        onShootInput?.Invoke(context.performed);
    }
}
