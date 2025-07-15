using Cysharp.Threading.Tasks;
using PaleLuna.Architecture.Initializer;
using Unity.Services.Authentication;
using Unity.Services.Core;

public class UnityServiceInit : InitializerBaseMono
{
    public override void StartInit()
    {
        _status = InitStatus.Initialization;

        _ = Init();
    }

    private async UniTaskVoid Init()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        
        _status = InitStatus.Done;
    }
}
