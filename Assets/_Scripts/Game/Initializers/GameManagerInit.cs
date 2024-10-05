using PaleLuna.Architecture.Initializer;
using Services;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class GameManagerInit : MonoBehaviour, IInitializer
{
    [SerializeField]
    private GameManager _gameManager;

    private InitStatus _status = InitStatus.Shutdown;
    public InitStatus status => _status;

    private void OnValidate()
    {
        _gameManager ??= GetComponent<GameManager>();
    }

    public void StartInit()
    {
        _gameManager.OnStart();
        ServiceManager.Instance.SceneLocator.Registarion(_gameManager);
        _status = InitStatus.Done;
    }
}
