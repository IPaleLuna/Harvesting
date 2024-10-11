using Services;
using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _minTextLabel;  
    [SerializeField]
    private TextMeshProUGUI _secTextLabel;

    private TimeBroadcaster _timeBroadcaster;

    private void Start()
    {
        _timeBroadcaster = ServiceManager.Instance.LocalServices.Get<TimeBroadcaster>();
        _timeBroadcaster.timeTickEvent.AddListener(TimeUpdate);
    }

    private void TimeUpdate(TimeStruct timeStruct)
    {
        _minTextLabel.SetText(timeStruct.min);
        _secTextLabel.SetText(timeStruct.sec);
    }
}
