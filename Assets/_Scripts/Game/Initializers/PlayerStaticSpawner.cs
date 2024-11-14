using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Services;
using Services;
using UnityEngine;

public class PlayerStaticSpawner : MonoBehaviour, IStartable
{
    [SerializeField]
    private Transform[] _playerTransforms;

    [SerializeField]
    private Transform[] _spawnPoints;

    private int pointer = 1;
    
    private bool _isStart = false;
    public bool IsStarted => _isStart;

    public void OnStart()
    {
        if (_isStart) return;
        _isStart = true;

        int playerCount = PlayerPrefs.GetInt(StringKeys.PLAYER_COUNT_KEY);
        int winnerId = ServiceManager.Instance
            .GlobalServices.Get<SceneLoaderService>()
            .GetCurrentBaggage().GetInt(StringKeys.WINNING_PLAYER_ID);

        for(int i = 0; i < playerCount; i++)
        {
            if(i == winnerId)
                Instantiate(_playerTransforms[i], _spawnPoints[0]);
            else
                Instantiate(_playerTransforms[i], _spawnPoints[pointer++]);
            
        }
    }
}
