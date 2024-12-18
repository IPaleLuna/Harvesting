using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using PaleLuna.Architecture.Initializer;
using PaleLuna.Network;
using UnityEngine;

public class NetworkObjectInit : InitializerBaseMono
{
    [SerializeReference]
    private List<NetworkLunaBehaviour> _networkBehaviours;

    private bool[] _statuses;

    private int _counter = 0;
    
    public override void StartInit()
    {
        _status = InitStatus.Initialization;
        _statuses = new bool[_networkBehaviours.Count];
        
        _ = InitNetworkBehaviours();
    }

    private async UniTaskVoid InitNetworkBehaviours()
    {
        while (_counter < _networkBehaviours.Count)
        {
            for (int i = 0; i < _networkBehaviours.Count; i++)
            {
                if(!_networkBehaviours[i].IsSpawned || _statuses[i]) continue;
                _statuses[i] = true;
                _counter++;
            }
            await UniTask.DelayFrame(1);
        }
        
        _networkBehaviours.ForEach(item => item.InitNetworkBehaviour());
        
        _status = InitStatus.Done;
    }
}
