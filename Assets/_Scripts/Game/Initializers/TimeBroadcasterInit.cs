using PaleLuna.Architecture.Initializer;
using Services;
using UnityEngine;

[RequireComponent(typeof(TimeBroadcaster))]
public class TimeBroadcasterInit : MonoBehaviour, IInitializer
{
    [SerializeField]
    private TimeBroadcaster _timeBroadcaster;

    private InitStatus _status = InitStatus.Shutdown;
    public InitStatus status => _status;

    private void OnValidate()
    {
        _timeBroadcaster ??= GetComponent<TimeBroadcaster>();
    }

    public void StartInit()
    {
        _timeBroadcaster.OnStart();

        ServiceManager.Instance.SceneLocator.Registarion(_timeBroadcaster);

        _status = InitStatus.Done;
    }
}
