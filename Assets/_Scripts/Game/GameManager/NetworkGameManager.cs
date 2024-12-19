using System.Collections;
using System.Collections.Generic;
using Harvesting.Game.GameTimer;
using Harvesting.Networking;
using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Services;
using PaleLuna.Network;
using Services;
using UnityEngine;

public class NetworkGameManager : NetworkLunaBehaviour, IService, IStartable
{
    [SerializeField]
    private BackstageScreen _backstageScreen;
    
    private ITimeController _timeController;
    
    public bool IsStarted { get; private set; } = false;
    
    public override void InitNetworkBehaviour()
    {
        ServiceManager.Instance.LocalServices.Registarion(this);
    }

    public void OnStart()
    {
        if(IsStarted || !IsServer) return;
        IsStarted = true;
        
        _timeController = ServiceManager
            .Instance.LocalServices
            .Get<TimeHandler>()
            .timeController;

        GlobalTimeEvents.onGameTimerFinished += OnGameTimeFinished;
        GlobalTimeEvents.onAfterGameTimerFinished += ReturnToLobby;
        
        _timeController.StartGameTimer();
    }
    
    private void OnGameTimeFinished()
    {
        GameEvents.timeOutEvent.Invoke();

        _timeController.StartAfterGameTimer();
    }

    private void ReturnToLobby()
    {
        _backstageScreen.FadeOut(() =>
        {
            ServiceManager.Instance.GlobalServices.Get<NetworkSceneManager>().SwitchScene("Lobby");
        });
    }

    
}
