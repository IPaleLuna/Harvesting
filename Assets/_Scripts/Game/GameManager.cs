using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Loops;
using PaleLuna.Architecture.Services;
using PaleLuna.Patterns.State.Game;
using PaleLuna.Timers.Implementations;
using Services;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour, IStartable, IService
{
    [SerializeField]
    private BackstageScreen _startScreen;

    [SerializeField]
    private int _gameTimeInSeconds = 120;
    [SerializeField]
    private int _timeToNextScene;

    
    private GameLoops _gameLoops;
    
    private AsyncTimer _timer;

    public bool IsStarted { get; private set; } = false;
    public AsyncTimer timer => _timer;

    public void OnStart()
    {
        if (IsStarted) return;
        IsStarted = true;

        _timer = new(_gameTimeInSeconds, OnTimeOut);
        _gameLoops = ServiceManager.Instance.GlobalServices.Get<GameLoops>();
        
        GameEvents.gameOnPauseEvent.AddListener(OnPause);
        GameEvents.exitSessionEvent.AddListener(OnExit);

        _timer.Start();
    }

    private void OnTimeOut()
    {
        GameEvents.timeOutEvent.Invoke();

        _timer = new(_timeToNextScene, LoadNextScene);
        _timer.Start();
    }

    private void OnPause(bool isPaused)
    {
        print(isPaused);
        if(isPaused)
        {
            _timer.OnPause();
            _gameLoops.stateHolder.ChangeState<PauseState>();
        }
        else
        {
            _timer.OnResume();
            _gameLoops.stateHolder.ChangeState<PlayState>();
        }
    }
    private void OnExit()
    {
        OnPause(true);
        
        ServiceManager.Instance.GlobalServices.Get<SceneLoaderService>().LoadSceneAsync(1, false);
        _startScreen.FadeOut(() =>
            ServiceManager.Instance.GlobalServices.Get<SceneLoaderService>().AllowNextScene()
            );
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
