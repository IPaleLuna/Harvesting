using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Services;
using Services;
using UnityEngine;

public class SetPlayerCountMode : MonoBehaviour, IStartable
{
    private SceneBaggage _baggage;

    private const int DEFAULT_PLAYER_COUNT = 2;

    private bool _isStarted = false;
    public bool IsStarted => _isStarted;

    public void OnStart()
    {
        _baggage = ServiceManager.Instance
            .GlobalServices.Get<SceneLoaderService>()
            .GetNextBaggage();

        _baggage.SetInt(StringKeys.PLAYER_COUNT_KEY, DEFAULT_PLAYER_COUNT);
    }

    public void OnModeChange(IOptionalData<int> data)
    {
        _baggage.SetInt(StringKeys.PLAYER_COUNT_KEY, data.GetData(StringKeys.PLAYER_COUNT_KEY));
    }
}
