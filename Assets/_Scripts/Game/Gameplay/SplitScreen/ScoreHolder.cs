using PaleLuna.Architecture.Services;
using Services;
using UnityEngine;

public class ScoreHolder : MonoBehaviour, IService
{
    private PlayerController _playerLeader = null;

    public PlayerController playerLeader => _playerLeader;

    private void Start()
    {
        ServiceManager.Instance.LocalServices.Registarion(this);
        GameEvents.playerPickApple.AddListener(OnPlayerPickUpApple);
    }


    private void OnPlayerPickUpApple(PlayerController playerPickedApple)
    {
        if (_playerLeader != null)
        {
            PlayerInfo leaderInfo = _playerLeader.playerInfo;
            PlayerInfo playerInfo = playerPickedApple.playerInfo;
            
            _playerLeader = leaderInfo.appleAmount >= playerInfo.appleAmount ? playerLeader : playerPickedApple;
        }
        else
            _playerLeader = playerPickedApple;
    }
    

    public void OnGameEnd()
    {
        ServiceManager.Instance
            .GlobalServices.Get<SceneLoaderService>()
            .GetNextBaggage()
            .SetInt(StringKeys.WINNING_PLAYER_ID, _playerLeader.playerInfo.playerID)
            .SetInt(StringKeys.MAX_SCORE_IN_SESSION, _playerLeader.playerInfo.appleAmount);
    }
}
