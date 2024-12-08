using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Loops;
using Services;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class MonoPlayerHandler : MonoBehaviour, IPausable
{
    [SerializeField]
    private PlayerController _playerController;
    
    private PlayerHandler _playerHandler;
    private GameLoops _gameLoops;

    private void OnValidate()
    {
        _playerController ??= GetComponent<PlayerController>();
    }

    private void Awake()
    {
        _gameLoops = ServiceManager.Instance.GlobalServices.Get<GameLoops>();
        _gameLoops.pausablesHolder.Registration(this);
        
        _playerHandler = new(_playerController);
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
        _gameLoops.pausablesHolder.Unregistration(this);
    }
    #region [ Pausable implementation ]
    public void OnPause()
    {
        _playerHandler.DisableControl();
    }
    public void OnResume()
    {
        _playerHandler.EnableControl();
    }
    #endregion
    
}
