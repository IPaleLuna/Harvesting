using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Harvesting.Game.GameTimer;
using UnityEngine;

public class MonoTimerController : MonoBehaviour, ITimeController
{
    [SerializeField]
    private int _gameTimeInSeconds;
    [SerializeField]
    private int _afterGameTimeInSeconds;
    
    private GameTimer _gameTimer;

    private bool isInit = false;
    private bool isAwait = false;


    public void Awake()
    {
        _gameTimer = new GameTimer(_gameTimeInSeconds, _afterGameTimeInSeconds);

        _gameTimer.onGameTimerFinished += () => GlobalTimeEvents.onGameTimerFinished?.Invoke();
        _gameTimer.onAfterGameTimerFinished += () => GlobalTimeEvents.onAfterGameTimerFinished?.Invoke();
        _gameTimer.onTick += (time) => GlobalTimeEvents.onTick?.Invoke(time);
        
        isInit = true;
    }

    public void Init()
    {
        throw new NotImplementedException();
    }

    public void StartGameTimer()
    {
        if (isInit) _gameTimer.StartGameTimer(); 
        _ = DelayedStart();
    }

    public void StartAfterGameTimer()
    {
        _gameTimer.StartAfterGameTimer();
    }

    public void Pause()
    {
        _gameTimer.PauseGameTimer();
    }

    public void Resume()
    {
        _gameTimer.ResumeGameTimer();
    }

    private async UniTaskVoid DelayedStart()
    {
        if(isAwait) return;
        isAwait = true;
        
        while (!isInit)
            await UniTask.Yield();
        
        StartGameTimer();        
    }
}
