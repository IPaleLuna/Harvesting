using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Loops;
using Services;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("components")]
    [SerializeField]
    private Rigidbody2D _rigidbody2D;
    [SerializeField]
    private PlayerInput _playerInput;

    [Header("Properties")]
    [SerializeField]
    private float _speed = 1.0F;
    public bool canMove = true;

    private PlayerInputActions _playerActions;
    private GameLoops _gameLoops;

    private Vector2 _currentDirection;

    public PlayerInputActions inputActions => _playerActions;
    public PlayerInput playerInput => _playerInput;

    private void OnValidate()
    {
        _rigidbody2D ??= GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerActions = new(_playerInput);
        _gameLoops = ServiceManager.Instance.GlobalServices.Get<GameLoops>();
    }

    private void OnEnable()
    {
        Subscribe();
    }

    public void Move()
    {
        if (!canMove) return;
        Vector2 velocity = _currentDirection * _speed;

        _rigidbody2D.velocity = velocity;
    }

    public void Stop()
    {
        _rigidbody2D.velocity = Vector2.zero;
        canMove = false;
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
