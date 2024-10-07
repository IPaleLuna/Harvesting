using PaleLuna.Architecture.Services;
using Services;
using UnityEngine;

public class ScoreHolder : MonoBehaviour, IService
{
    private Player _playerWithMaxScore = null;

    public Player playerWithMaxScore => _playerWithMaxScore;

    private void Start()
    {
        ServiceManager.Instance.SceneLocator.Registarion(this);
        GameEvents.applePickEvent.AddListener(OnPlayerPickUpApple);
    }


    private void OnPlayerPickUpApple(Player player)
    {
        if (_playerWithMaxScore != null)
            _playerWithMaxScore = _playerWithMaxScore.applesAmount < player.applesAmount ? player : _playerWithMaxScore;
        else
            _playerWithMaxScore = player;
    }

    private void OnGameEnd()
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.WINNING_PLAYER_ID, _playerWithMaxScore.playerID);
    }
}
