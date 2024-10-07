using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Services;
using PaleLuna.Timers.Implementations;
using Services;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour, IStartable, IService
{
    [SerializeField]
    private int gameTimeInSeconds = 120;
    [SerializeField]
    private SpaceKeyListener _spaceListener;

    private AsyncTimer _timer;

    private bool _isStart = false;
    public bool IsStarted => _isStart;
    public AsyncTimer timer => _timer;

    public void OnStart()
    {
        if (_isStart) return;
        _isStart = true;


        _timer = new(gameTimeInSeconds, OnTimeOut);

        _timer.Start();
    }

    private void OnTimeOut()
    {
        GameEvents.timeOutEvent.Invoke();

        _spaceListener.spaceAction.performed += RestartGame;
    }

    public void RestartGame(InputAction.CallbackContext context)
    {
        ServiceManager.Instance.GlobalServices.Get<SceneLoaderService>().LoadScene(1);
    }
}
