using System;
using Harvesting.PlayerHandler;
using PaleLuna.Network;
using PaleLuna.Timers.Implementations;
using UnityEngine;
using UnityEngine.Serialization;


public class StartGameTigger : NetworkLunaBehaviour
{
    private int playerCounter = 0;

    [FormerlySerializedAs("timer")] [SerializeField]
    private TickCounter _tickCounter = new TickCounter();
    
    public override void InitNetworkBehaviour()
    {
        if (!IsServer)
        {
            Destroy(this);
            return;
        }
        
        _tickCounter.SetUp(LoadGameScene);
        _tickCounter.onEveryTickAction += SendTimer;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<NetworkPlayerView>() == null) return;
        
        playerCounter++;

        if (playerCounter > 1 && playerCounter == NetworkManager.ConnectedClients.Count)
            _tickCounter.Start();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<NetworkPlayerView>() == null) return;
        if(playerCounter == NetworkManager.ConnectedClients.Count)
            _tickCounter.ShutDown();
        
        playerCounter--;
    }

    private void SendTimer()
    {
        print($"Wait for... {_tickCounter.targetTicks - _tickCounter.currentTicks}");
    }

    private void LoadGameScene()
    {
        _tickCounter.ShutDown();
        print("Loading Game Scene");
        
    }
    
    
}
