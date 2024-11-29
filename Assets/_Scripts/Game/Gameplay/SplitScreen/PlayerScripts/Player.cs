using System;
using NaughtyAttributes;
using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Loops;
using Services;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour, IFixedUpdatable, IPausable
{
    [Header("Auto filling components"), HorizontalLine(color: EColor.Gray)]
    [SerializeField]
    private PlayerController _controller;

    [Header("Characteristics"), HorizontalLine(color: EColor.Violet)]
    [SerializeField]
    private PlayerCharacteristics _characteristics;

    private readonly Basket _basketOfApples = new();
    private GameLoops _gameLoops;
    public int applesAmount => _basketOfApples.appleAmount;
    public int playerID => _controller.playerInput.playerIndex;
    public PlayerCharacteristics characteristics => _characteristics;

    private void OnValidate()
    {
        _controller ??= GetComponent<PlayerController>();
    }

    private void Awake()
    {
        _gameLoops = ServiceManager.Instance.GlobalServices.Get<GameLoops>();
        
        GameEvents.timeOutEvent.AddListener(DisableControll);
        _gameLoops.pausablesHolder.Registration(this);
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
            apple.DeactivateThis();
            GameEvents.playerPickApple.Invoke(this);
        }
    }

    private void DisableControll()
    {
        _gameLoops.Unregistration(this);
        _controller.Stop();
    }

    private void EnableControll()
    {
        _gameLoops.Registration(this);
        _controller.Run();
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
