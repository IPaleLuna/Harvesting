using NaughtyAttributes;
using PaleLuna.Architecture.Loops;
using Services;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    [Header("Components"), HorizontalLine(color: EColor.Gray)]
    [SerializeField]
    private Rigidbody2D _rigidbody2D;
    [SerializeField]
    private PlayerInput _playerInput;
    [SerializeField]
    private Player _player;

    private PlayerInputActions _playerActions;
    private GameLoops _gameLoops;

    private Vector2 _currentDirection;

    public PlayerInputActions inputActions => _playerActions;
    public PlayerInput playerInput => _playerInput;

    private void OnValidate()
    {
        _rigidbody2D ??= GetComponent<Rigidbody2D>();
        _playerInput ??= GetComponent<PlayerInput>();
        _player ??= GetComponent<Player>();
    }

    private void Awake()
    {
        _playerActions = new(_playerInput);
        _gameLoops = ServiceManager.Instance.GlobalServices.Get<GameLoops>();
    }

    private void OnEnable()
    {
        Subscribe();
    }

    public void Move()
    {
        Vector2 velocity = _currentDirection * _player.characteristics.speed;

        _rigidbody2D.velocity = velocity;
    }

    public void Stop()
    {
        _rigidbody2D.velocity = Vector2.zero;
    }

    private void OnGetMoveInput(InputAction.CallbackContext context)
    {
        _currentDirection = context.ReadValue<Vector2>();
    }

    private void Subscribe()
    {
        _playerActions.movementAction.performed += OnGetMoveInput;
        _playerActions.movementAction.canceled += OnGetMoveInput;
    }
    private void Unsubscribe()
    {
        _playerActions.movementAction.performed -= OnGetMoveInput;
        _playerActions.movementAction.canceled -= OnGetMoveInput;
    }

    private void OnDisable()
    {
        Unsubscribe();
    }
}
