using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Services;
using PaleLuna.Timers.Implementations;
using Services;
using UnityEngine;

public class GameManager : MonoBehaviour, IStartable, IService
{
    [SerializeField]
    private BackstageScreen _startScreen;

    [SerializeField]
    private int _gameTimeInSeconds = 120;
    [SerializeField]
    private int _timeToNextScene;

    private AsyncTimer _timer;

    private bool _isStart = false;
    public bool IsStarted => _isStart;
    public AsyncTimer timer => _timer;

    public void OnStart()
    {
        if (_isStart) return;
        _isStart = true;

        _timer = new(_gameTimeInSeconds, OnTimeOut);

        _timer.Start();
    }

    private void OnTimeOut()
    {
        GameEvents.timeOutEvent.Invoke();

        _timer = new(_timeToNextScene, LoadNextScene);
        _timer.Start();
    }

    public void LoadNextScene()
    {
        ServiceManager.Instance.LocalServices.Get<ScoreHolder>().OnGameEnd();
        ServiceManager.Instance.GlobalServices.Get<SceneLoaderService>().LoadSceneAsync(3, false);

        _startScreen.FadeOut(() =>
        {
            ServiceManager.Instance.GlobalServices.Get<SceneLoaderService>().AllowNextScene();
        });
    }
}
