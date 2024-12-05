using NaughtyAttributes;
using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Loops;
using Services;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour, IFixedUpdatable, IPausable
{
    [FormerlySerializedAs("movement")]
    [FormerlySerializedAs("_controller")]
    [Header("Auto filling components"), HorizontalLine(color: EColor.Gray)]
    [SerializeField]
    private PlayerMovement _movement;

    [Header("Characteristics"), HorizontalLine(color: EColor.Violet)]
    [SerializeField]
    private PlayerCharacteristics _characteristics;
    
    private GameLoops _gameLoops;
    
    private readonly PlayerModel _model = new();
    private PlayerView _view;
    
    public int applesAmount => _model.appleAmount;
    public int playerID => _movement.playerInput.playerIndex;

    private void OnValidate()
    {
        _movement ??= GetComponent<PlayerMovement>();
    }

    private void Awake()
    {
        
        _gameLoops = ServiceManager.Instance.GlobalServices.Get<GameLoops>();
        
        GameEvents.timeOutEvent.AddListener(DisableControll);
        _gameLoops.pausablesHolder.Registration(this);
        
        _model.speed = _characteristics.speed;
        _movement.SetModel(_model);
    }

    public void FixedFrameRun()
    {
        _movement.Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Apple apple))
        {
            _model.AddApples(apple.cost);
            apple.DeactivateThis();
            GameEvents.playerPickApple.Invoke(this);
        }
    }

    private void DisableControll()
    {
        _gameLoops.Unregistration(this);
        _movement.Stop();
    }

    private void EnableControll()
    {
        _gameLoops.Registration(this);
        _movement.Run();
    }

    private void OnDestroy()
    {
        _gameLoops.Unregistration(this);
        _gameLoops.pausablesHolder.Unregistration(this);
    }
    #region [ Pausable implementation ]
    public void OnPause()
    {
        DisableControll();
    }

    public void OnResume()
    {
        EnableControll();
    }

    #endregion
}
