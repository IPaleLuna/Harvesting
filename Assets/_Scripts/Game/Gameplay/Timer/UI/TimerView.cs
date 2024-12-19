using TMPro;
using UnityEngine;

namespace Harvesting.Game.GameTimer
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _minTextLabel;  
        [SerializeField]
        private TextMeshProUGUI _secTextLabel;

        private void Start()
        {
            GlobalTimeEvents.onTick += TimeUpdate;
        }

        private void TimeUpdate(TimeStruct timeStruct)
        {
            _minTextLabel.SetText(timeStruct.min);
            _secTextLabel.SetText(timeStruct.sec);
        }
    }
}

