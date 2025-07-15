using Cysharp.Threading.Tasks;
using PaleLuna.Architecture.Initializer;
using UnityEngine;

namespace Harvesting.Game.GameTimer
{
    [RequireComponent(typeof(TimeHandler))]
    public class TimerInit : InitializerBaseMono
    {
        [SerializeField]
        private TimeHandler _timeHandler;

        private void OnValidate()
        {
            _timeHandler ??= GetComponent<TimeHandler>();
        }

        public override void StartInit()
        {
            _status = InitStatus.Initialization;
        
            _timeHandler.OnStart();

            _ = Init();
        }

        private async UniTaskVoid Init()
        {
            while (!_timeHandler.timeController.isInit)
                await UniTask.DelayFrame(1);
            
            _status = InitStatus.Done;
        }
    }
}


