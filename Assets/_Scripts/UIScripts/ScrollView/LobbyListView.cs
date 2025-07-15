using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using Unity.Services.Lobbies.Models;
using UnityEngine;

[RequireComponent(typeof(ListView))]
public class LobbyListView : MonoBehaviour
{
    [SerializeField]
    private ListView _listView;
    
    [Header("List update parameters"), HorizontalLine(color: EColor.Blue)]
    [SerializeField]
    private TickCounter _tickCounter = new();

    private readonly LobbySearcher _lobbySearcher = new();

    private void OnValidate()
    {
        _listView ??= GetComponent<ListView>();
    }
    private void Awake()
    {
        _listView.Init();
        _tickCounter.SetUp(OnTick);
    }

    private void OnEnable()
    {
        _ = RefreshList();
        _tickCounter.Start();
    }

    private void OnDisable()
    {
        _tickCounter.ShutDown();
    }

    private async UniTaskVoid RefreshList()
    {
        List<Lobby> lobbies = await _lobbySearcher.Search();
        _listView.Refresh(lobbies.Count);

        for (int i = 0; i < lobbies.Count; i++)
        {
            _listView.activeItems[i]
                .GetComponent<LobbyItemController>()
                .UpdateLobbyItemData(lobbies[i]);
        }
    }

    private void OnTick()
    {
        _ = RefreshList();
    }
}
