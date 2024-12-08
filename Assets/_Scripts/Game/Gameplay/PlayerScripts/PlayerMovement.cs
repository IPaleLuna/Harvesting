using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [Header("Main Components"), HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private Rigidbody2D _rigidbody2D;
    [SerializeField]
    private PlayerInput _playerInput;

    public bool isDirectionChanged { get; private set; } = false;

    private PlayerInputActions _playerActions;

    public Vector2 currentDirection { get; private set;}
    
    public PlayerInput playerInput => _playerInput;
    
    private Vector2 _lastDirection = Vector2.zero;

    private void OnValidate()
    {
        _rigidbody2D ??= GetComponent<Rigidbody2D>();
        _playerInput ??= GetComponent<PlayerInput>();
    }

    public void Init()
    {
        _playerInput.enabled = true;
        
        _playerActions = new(_playerInput);
        Subscribe();
    }


    public void Move(float speed)
    {
        Vector2 velocity = currentDirection * speed;
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
        currentDirection = context.ReadValue<Vector2>();

        isDirectionChanged = _lastDirection != currentDirection;
        if (isDirectionChanged)
            _lastDirection = currentDirection;            
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

    private void OnEnable()
    {
        //Subscribe();
    }
    
    private void OnDisable()
    {
        //Unsubscribe();
    }

    public void Remove()
    {
        Destroy(_playerInput);
        Destroy(this);
    }
}
