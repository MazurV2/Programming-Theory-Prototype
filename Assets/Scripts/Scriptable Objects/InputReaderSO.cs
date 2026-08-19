using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReaderSO", menuName = "Input/Input Reader")]
public class InputReaderSO : ScriptableObject
{
    private Controls _controls;

    public event Action<Vector2> onMoveInput;
    public event Action<bool> onShootInput;

    private void OnEnable()
    {
        _controls = new Controls();
        _controls.Enable();

        _controls.Player.Move.performed += Move;
        _controls.Player.Move.canceled += Move;

        _controls.Player.Shoot.performed += Shoot;
        _controls.Player.Shoot.canceled += Shoot;
    }

    private void OnDisable()
    {
        _controls.Player.Move.performed -= Move;
        _controls.Player.Move.canceled -= Move;

        _controls.Player.Shoot.performed -= Shoot;
        _controls.Player.Shoot.canceled -= Shoot;

        _controls.Disable();
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
