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

            _status = InitStatus.Done;
        }
    }
}


