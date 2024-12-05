using Harvesting.Networking;
using PaleLuna.Architecture.Initializer;
using Services;

namespace Harvesting.Initializers
{
    public class NetworkLobbyManagerInit : InitializerBaseMono
    {
        public override void StartInit()
        {
            _status = InitStatus.Initialization;
            NetworkLobbyManager networkLobbyManager = new NetworkLobbyManager();

            networkLobbyManager.OnStart();

            ServiceManager.Instance
                .GlobalServices
                .Registarion(networkLobbyManager);

            _status = InitStatus.Done;
        }
    }
}
