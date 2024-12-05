using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputActions
{
    private const string MOVEMENT_KEY = "Movement";

    private PlayerInput _playerInput;

    private InputAction _movementAction;

    public InputAction movementAction => _movementAction;

    public PlayerInputActions(PlayerInput playerInput)
    {
        _playerInput = playerInput;

        _movementAction = _playerInput.actions[MOVEMENT_KEY];
    }
}
