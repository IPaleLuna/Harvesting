using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Loops;
using Services;
using Unity.Netcode;
using UnityEngine;


public class NetWorkPlayerHandler : NetworkBehaviour
{
    [SerializeField]
    private PlayerController _playerController;
    
    private PlayerHandler _playerHandler;
    
    private GameLoops _gameLoops;

    private void OnValidate()
    {
        _playerController ??= GetComponent<PlayerController>();
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            _playerController.Remove();
            Destroy(this);
            return;
        }

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
}
