using Cysharp.Threading.Tasks;
using PaleLuna.Architecture.Initializer;
using Services;
using UnityEngine;


public class DataBaseInitializer : InitializerBaseMono
{
    [SerializeField]
    private HttpUrls _urls;

    public override void StartInit()
    {
        _status = InitStatus.Initialization;
        Init();
    }

    private void Init()
    {
        PlayerScoreTable playerScoreTable = new(_urls);

        ServiceManager.Instance.GlobalServices.Registarion(playerScoreTable);

        _status = InitStatus.Done;
    }
}
