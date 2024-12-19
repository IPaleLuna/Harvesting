using PaleLuna.Network;
using Services;
using Unity.Netcode;
using UnityEngine;
using NetworkSceneManager = Harvesting.Networking.NetworkSceneManager;


public class StartGameTigger : NetworkLunaBehaviour
{
    [SerializeField]
    private CountdownView _countdownView;

    [SerializeField]
    private bool _isDebug = false;
    
    [SerializeField]
    private BackstageScreen _backstageScreen;
    
    [SerializeField]
    private TickCounter _tickCounter = new TickCounter();
    
    
    private int playerCounter = 0;
    
    
    public override void InitNetworkBehaviour()
    {
        if (!IsServer) return;
        
        _tickCounter.SetUp(LoadGameScene);
        _tickCounter.onEveryTickAction += SendTimer;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<NetworkPlayerView>() == null || !IsServer) return;
        
        playerCounter++;
        
        print($"{playerCounter}/{NetworkManager.ConnectedClients.Count}");

        if ((_isDebug || playerCounter > 1) && playerCounter == NetworkManager.ConnectedClients.Count)
        {
            _tickCounter.Start();
            print("StartGameTigger");            
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<NetworkPlayerView>() == null || !IsServer) return;
        
        if(playerCounter == NetworkManager.ConnectedClients.Count)
            _tickCounter.ShutDown();
        
        playerCounter--;
    }

    private void SendTimer()
    {
        _countdownView
            .SetCountdownTextClientRpc(NumToStringBuffer
                .GetIntToStringHash(
                    _tickCounter.targetTicks - _tickCounter.currentTicks
                    )
            );
    }

    private void LoadGameScene()
    {
        _tickCounter.ShutDown();
        SendBackStageClientRpc();
        
        if (IsServer)
        {
            _backstageScreen.FadeOut(() => 
                ServiceManager.Instance.
                    GlobalServices.Get<NetworkSceneManager>()
                    .SwitchScene("NetGameScene"));
        }
    }

    [ClientRpc]
    private void SendBackStageClientRpc()
    {
        _backstageScreen.FadeOut();
    }
    
    
}
