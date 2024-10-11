using PaleLuna.Architecture.Initializer;
using Services;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class GameManagerInit : InitializerBaseMono
{
    [SerializeField]
    private GameManager _gameManager;

    private void OnValidate()
    {
        _gameManager ??= GetComponent<GameManager>();
    }

    public override void StartInit()
    {
        _gameManager.OnStart();
        ServiceManager.Instance.LocalServices.Registarion(_gameManager);
        _status = InitStatus.Done;
    }
}
