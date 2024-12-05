using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Loops;
using Services;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class MonoPlayerHandler : MonoBehaviour, IFixedUpdatable, IPausable
{
    [SerializeField]
    private PlayerController _playerController;
    
    private GameLoops _gameLoops;

    private void OnValidate()
    {
        _playerController ??= GetComponent<PlayerController>();
    }

    private void Awake()
    {
        _gameLoops = ServiceManager.Instance.GlobalServices.Get<GameLoops>();
        
        GameEvents.timeOutEvent.AddListener(DisableControl);
        _gameLoops.pausablesHolder.Registration(this);
    }
    
    public void FixedFrameRun()
    {
        _playerController.Move();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Apple apple))
        {
            _playerController.CollectApple(apple);
            apple.DeactivateThis();
            GameEvents.playerPickApple.Invoke(_playerController);
        }
    }
    
    private void OnDestroy()
    {
        _gameLoops.Unregistration(this);
        _gameLoops.pausablesHolder.Unregistration(this);
    }
    #region [ Pausable implementation ]
    public void OnPause()
    {
        DisableControl();
    }

    public void OnResume()
    {
        EnableControl();
    }

    #endregion
    
    private void EnableControl()
    {
        _gameLoops.Registration(this);
        _playerController.IsActive(true);
    }
    private void DisableControl()
    {
        _gameLoops.Unregistration(this);
        _playerController.IsActive(false);
    }
}
