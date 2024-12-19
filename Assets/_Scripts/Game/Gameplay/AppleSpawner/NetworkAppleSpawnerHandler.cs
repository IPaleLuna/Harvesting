using Cysharp.Threading.Tasks;
using Harvesting.Utility.Spawner;
using PaleLuna.Network;
using UnityEngine;

public class NetworkAppleSpawnerHandler : NetworkLunaBehaviour
{
    [SerializeField]
    private AppleSpawnerModel _model;
    
    [Space, SerializeField]
    private TickCounter _tickHolder = new();
    
    private AppleSpawner _spawner;

    public override void InitNetworkBehaviour()
    {
        if (!IsServer)
        {
            Destroy(_model);
            Destroy(this);
            return;
        }
        
        _model.CollectTrees();
        _spawner = new AppleSpawner(_model, new NetSpawner<AppleHandler>());

        _ = FirstSpawn();
    }


    private async UniTask FirstSpawn()
    {
        await UniTask.WaitForSeconds(5);
                
        _tickHolder.SetUp(OnTick);
        _tickHolder.Start();
        
        OnTick();
    }

    private void OnTick()
    {
        _spawner.Spawn();
    }

    public override void OnDestroy()
    {
        _tickHolder.ShutDown();
        base.OnDestroy();
    }
}
