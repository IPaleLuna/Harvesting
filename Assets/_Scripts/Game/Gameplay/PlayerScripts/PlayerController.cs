using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Loops;
using Services;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IFixedUpdatable
{
    [Header("components")]
    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [Header("Properties")]
    [SerializeField]
    private float _speed = 1.0F;

    private PlayerInputActions _playerActions;
    private GameLoops _gameLoops;

    private Vector2 _currentDirection;

    private void OnValidate()
    {
        _rigidbody2D ??= GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        _playerActions = new(GetComponent<PlayerInput>());
        _gameLoops = ServiceManager.Instance.GlobalServices.Get<GameLoops>();
    }

    private void OnEnable()
    {
        Subscribe();
    }

    public void FixedFrameRun()
    {
        Vector2 velocity = _currentDirection * _speed;

        _rigidbody2D.velocity = velocity;
    }

    private void OnGetMoveInput(InputAction.CallbackContext context)
    {
        _currentDirection = context.ReadValue<Vector2>();
    }

    private void Subscribe()
    {
        _gameLoops.Registration(this);

        _playerActions.movementAction.performed += OnGetMoveInput;
        _playerActions.movementAction.canceled += OnGetMoveInput;
    }
    private void Unsubscribe()
    {
        _gameLoops.Unregistration(this);

        _playerActions.movementAction.performed -= OnGetMoveInput;
        _playerActions.movementAction.canceled -= OnGetMoveInput;
    }

    private void OnDisable()
    {
        Unsubscribe();
    }
}
