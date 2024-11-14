using NaughtyAttributes;
using PaleLuna.Architecture.Loops;
using Services;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Main Components"), HorizontalLine(color: EColor.Violet)]
    [SerializeField]
    private Rigidbody2D _rigidbody2D;
    [SerializeField]
    private PlayerInput _playerInput;
    [SerializeField]
    private Player _player;

    [Header("Additions components"), HorizontalLine(color: EColor.Gray)]
    [SerializeField]
    private SpriteFlipper _spriteFlipper;
    [SerializeField]
    private AnimationControll _animationControl;

    private PlayerInputActions _playerActions;

    private Vector2 _currentDirection;
    
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

    public void Run()
    {
        Subscribe();
    }
    public void Stop()
    {
        _rigidbody2D.velocity = Vector2.zero;
        Unsubscribe();
    }

    private void OnGetMoveInput(InputAction.CallbackContext context)
    {
        _currentDirection = context.ReadValue<Vector2>();

        _spriteFlipper.OnInputDirectionChanged(_currentDirection);
        _animationControl.OnInputDirectionChanged(_currentDirection);
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
