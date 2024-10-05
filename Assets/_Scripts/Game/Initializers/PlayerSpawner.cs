using Cysharp.Threading.Tasks;
using PaleLuna.Architecture.Initializer;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerSpawner : MonoBehaviour, IInitializer
{
    [SerializeReference]
    private List<Transform> _spawnPoints;

    private InitStatus _status = InitStatus.Shutdown;
    public InitStatus status => _status;

    private string _playerPrefabsPath = "Prefabs/Players/Player";
    private int _playerCount = 0;

    public void StartInit()
    {
        _status = InitStatus.Initialization;

        _playerCount = PlayerPrefs.GetInt(PlayerPrefsKeys.PLAYER_COUNT_KEY);

        _ = InitPlayers();
    }

    private async UniTaskVoid InitPlayers()
    {
        for(int i = 0; i < _playerCount; i++)
        {
            string playerConcretePath = _playerPrefabsPath + $" {i+1}";

            ResourceRequest request = Resources.LoadAsync<GameObject>(playerConcretePath);

            await request;

            if(request.asset == null)
                throw new ArgumentNullException($"path: {playerConcretePath} dont contain GameObject");

            CreatePlayer(request.asset as GameObject);
        }

        _status = InitStatus.Done;
    }

    private void CreatePlayer(GameObject playerPrefab)
    {
        PlayerInput playerInput = Instantiate(playerPrefab).GetComponentInChildren<PlayerInput>();

        if (playerInput.playerIndex < 2)
            playerInput.SwitchCurrentControlScheme(playerInput.currentControlScheme, Keyboard.current);

        int point = Random.Range(0, _spawnPoints.Count);
        playerInput.transform.position = _spawnPoints[point].position;

        _spawnPoints.RemoveAt(point);
    }

}
