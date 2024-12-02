using NetWorking;
using PaleLuna.Architecture.Initializer;
using Services;

public class NetcodeSceneManagerInit : InitializerBaseMono
{
    public override void StartInit()
    {
        _status = InitStatus.Initialization;
        
        Init();
    }

    private void Init()
    {
        NetworkSceneManager networkSceneManager = new NetworkSceneManager();
        ServiceManager.Instance.GlobalServices.Registarion(networkSceneManager);
        
        _status = InitStatus.Done;
    }
}
