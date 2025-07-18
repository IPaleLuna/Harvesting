using Cysharp.Threading.Tasks;
using Unity.Collections;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace Harvesting.Game.GameTimer
{
    public class NetworkTimeHandler : NetworkBehaviour, ITimeController
    {
        [SerializeField]
        private int _gameTimeInSeconds;
        [SerializeField]
        private int _afterGameTimeInSeconds;
        
        [SerializeField]
        private bool _autoRun = true;
        
        private GameTimer _gameTimer;
    
        public bool isInit { get; private set; }
        private bool _isAwait = false;

        public override void OnNetworkSpawn()
        {
            if (!IsServer)
            {
                isInit = true;
                return;
            }
            _gameTimer = new GameTimer(_gameTimeInSeconds, _afterGameTimeInSeconds);
    
            _gameTimer.onGameTimerFinished += () => GlobalTimeEvents.onGameTimerFinished?.Invoke();
            _gameTimer.onAfterGameTimerFinished += () => GlobalTimeEvents.onAfterGameTimerFinished?.Invoke();
            _gameTimer.onTick += (time) => UpdateTimerClientRpc(NetTimeStruct.Convert(time));
            
            isInit = true;
            
            if(_autoRun) StartGameTimer();
        }
    
        public void StartGameTimer()
        {
            if(!IsServer) return;
            
            _gameTimer.StartGameTimer();
        }

        public void StartAfterGameTimer()
        {
            if(IsServer) _gameTimer.StartAfterGameTimer();
        }

        public void Pause()
        {
            if(IsServer) _gameTimer.PauseGameTimer();
        }

        public void Resume()
        {
            if(IsServer) _gameTimer.ResumeGameTimer();
        }

        [ClientRpc]
        private void UpdateTimerClientRpc(NetTimeStruct netTime) => GlobalTimeEvents.onTick?.Invoke(NetTimeStruct.Convert(netTime));
    }

    public struct NetTimeStruct : INetworkSerializable
    {
        public FixedString32Bytes min;
        public FixedString32Bytes sec;
        
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref min);
            serializer.SerializeValue(ref sec);
        }

        public NetTimeStruct(FixedString32Bytes min, FixedString32Bytes sec)
        {
            this.min = min;
            this.sec = sec;
        }

        public static NetTimeStruct Convert(TimeStruct time)
        {
            return new NetTimeStruct(new FixedString32Bytes(time.min), new FixedString32Bytes(time.sec));
        }

        public static TimeStruct Convert(NetTimeStruct time)
        {
            return new TimeStruct(time.min.ToString(), time.sec.ToString());
        }
    }
}

