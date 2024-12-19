using Harvesting.Game.GameTimer;
using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Loops;
using PaleLuna.Architecture.Services;
using PaleLuna.Patterns.State.Game;
using Services;
using UnityEngine;

public class GameManager : MonoBehaviour, IStartable, IService
{
    [SerializeField]
    private BackstageScreen _startScreen;

    [SerializeField]
    private ITimeController _timeController;
    
    private GameLoops _gameLoops;

    public bool IsStarted { get; private set; } = false;

    public void OnStart()
    {
        if (IsStarted) return;
        IsStarted = true;

        _timeController = ServiceManager.Instance
            .LocalServices.Get<TimeHandler>()
            ?.timeController;

        _gameLoops = ServiceManager.Instance.GlobalServices.Get<GameLoops>();
        
        GameEvents.gameOnPauseEvent.AddListener(OnPause);
        GameEvents.exitSessionEvent.AddListener(OnExit);

        GlobalTimeEvents.onGameTimerFinished += OnTimeOut;
        GlobalTimeEvents.onAfterGameTimerFinished += LoadNextScene;
    }

    private void OnTimeOut()
    {
        GameEvents.timeOutEvent.Invoke();

        _timeController.StartAfterGameTimer();
    }

    private void OnPause(bool isPaused)
    {
        print(isPaused);
        if(isPaused)
        {
            _timeController.Pause();
            _gameLoops.stateHolder.ChangeState<PauseState>();
        }
        else
        {
            _timeController.Resume();
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

    private void LoadNextScene()
    {
        ServiceManager.Instance.LocalServices.Get<ScoreHolder>().OnGameEnd();
        ServiceManager.Instance.GlobalServices.Get<SceneLoaderService>().LoadSceneAsync(3, false);

        _startScreen.FadeOut(() =>
        {
            ServiceManager.Instance.GlobalServices.Get<SceneLoaderService>().AllowNextScene();
        });
    }
}
