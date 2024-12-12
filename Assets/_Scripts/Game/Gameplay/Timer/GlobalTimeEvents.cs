using System;

namespace Harvesting.Game.GameTimer
{
    public static class GlobalTimeEvents
    {
        public static Action onGameTimerFinished { get; set; }
        public static Action onAfterGameTimerFinished { get; set; }
        public static Action<TimeStruct> onTick { get; set; }
    }
}


