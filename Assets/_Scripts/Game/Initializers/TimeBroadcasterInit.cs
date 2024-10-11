using PaleLuna.Architecture.Initializer;
using Services;
using UnityEngine;

[RequireComponent(typeof(TimeBroadcaster))]
public class TimeBroadcasterInit : InitializerBaseMono
{
    [SerializeField]
    private TimeBroadcaster _timeBroadcaster;

    private void OnValidate()
    {
        _timeBroadcaster ??= GetComponent<TimeBroadcaster>();
    }

    public override void StartInit()
    {
        _timeBroadcaster.OnStart();

        ServiceManager.Instance.LocalServices.Registarion(_timeBroadcaster);

        _status = InitStatus.Done;
    }
}
