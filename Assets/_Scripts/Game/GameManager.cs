using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Services;
using PaleLuna.Timers.Implementations;
using Services;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour, IStartable, IService
{
    [SerializeField]
    private int gameTimeInSeconds = 120;

    private AsyncTimer _timer;

    private bool _isStart = false;
    public bool IsStarted => _isStart;
    public AsyncTimer timer => _timer;

    public void OnStart()
    {
        if (_isStart) return;
        _isStart = true;

        ServiceManager.Instance.SceneLocator.Registarion(this);

        _timer = new(gameTimeInSeconds, OnGameEnd);

        _timer.Start();
    }

    private void OnGameEnd()
    {

    }
}
