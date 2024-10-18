using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Loops;
using Services;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour, IFixedUpdatable
{
    [Header("Auto filling components")]
    [SerializeField]
    private PlayerController _controller;

    private readonly Basket _basketOfApples = new();
    private GameLoops _gameLoops;

    public int applesAmount => _basketOfApples.appleAmount;
    public int playerID => _controller.playerInput.playerIndex;

    private void OnValidate()
    {
        _controller ??= GetComponent<PlayerController>();
    }

    private void Awake()
    {
        _gameLoops = ServiceManager.Instance.GlobalServices.Get<GameLoops>();
        GameEvents.timeOutEvent.AddListener(DisableControll);
    }

    public void FixedFrameRun()
    {
        _controller.Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Apple apple))
        {
            _basketOfApples.AddApples(apple.cost);
            apple.PickThis();
            GameEvents.playerPickApple.Invoke(this);
        }
    }

    private void DisableControll()
    {
        _gameLoops.Unregistration(this);
        _controller.Stop();
    }

    #region [ Enable/Disable ]
    private void OnEnable()
    {
        _gameLoops.Registration(this);
    }
    private void OnDisable()
    {
        _gameLoops.Unregistration(this);
    }
    #endregion
}
