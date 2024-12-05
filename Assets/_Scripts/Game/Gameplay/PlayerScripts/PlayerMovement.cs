using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerView))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Main Components"), HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private Rigidbody2D _rigidbody2D;
    [SerializeField]
    private PlayerInput _playerInput;

    [Header("MVC components"), HorizontalLine(color: EColor.Violet)]
    [SerializeField]
    private PlayerView _view;
    private PlayerModel _model;

    private PlayerInputActions _playerActions;

    private Vector2 _currentDirection;
    
    public PlayerInput playerInput => _playerInput;

    private void OnValidate()
    {
        _rigidbody2D ??= GetComponent<Rigidbody2D>();
        _playerInput ??= GetComponent<PlayerInput>();
        _view ??= GetComponent<PlayerView>();
    }

    private void Awake()
    {
        _playerActions = new(_playerInput);
    }
    
    private void OnEnable()
    {
        Subscribe();
    }

    public void SetModel(PlayerModel model) => _model = model;

    public void Move()
    {
        Vector2 velocity = _currentDirection * _model.speed;

        _rigidbody2D.velocity = velocity;
    }

    public void Run()
    {
        Subscribe();
    }
    public void Stop()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _view.ResetAnimations();
        Unsubscribe();
    }

    private void OnGetMoveInput(InputAction.CallbackContext context)
    {
        _currentDirection = context.ReadValue<Vector2>();
        _view.UpdateDirection(_currentDirection);
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
