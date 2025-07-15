using Cysharp.Threading.Tasks;
using PaleLuna.Architecture.Initializer;
using PaleLuna.Architecture.Services;
using Services;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerSpawner : InitializerBaseMono
{
    [SerializeReference]
    private List<Transform> _spawnPoints;

    private string _playerPrefabsPath = "Prefabs/Players/Player";
    private int _playerCount = 0;

    public override void StartInit()
    {
        _status = InitStatus.Initialization;

        _playerCount = PlayerPrefs.GetInt(StringKeys.PLAYER_COUNT_KEY);

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

        if (playerInput.playerIndex < 3)
            playerInput.SwitchCurrentControlScheme(playerInput.currentControlScheme, Keyboard.current);

        int point = Random.Range(0, _spawnPoints.Count);
        playerInput.transform.position = _spawnPoints[point].position;

        SplitScreen(playerInput.GetComponentInChildren<Camera>(), playerInput.playerIndex);

        _spawnPoints.RemoveAt(point);
    }

    private void SplitScreen(Camera playerCam, int id)
    {

        if (_playerCount <= 3)
            SplitScreenVertical(playerCam, id);
        else
            SplitScreenVH(playerCam, id);
    }

    private void SplitScreenVertical(Camera playerCam, int id)
    {
        float width = 1f / _playerCount;
        playerCam.rect = new Rect(id * width, 0, width, 1);
    }
    private void SplitScreenVH(Camera playerCam, int id)
    {
        float x = (id % 2 == 0) ? 0 : 0.5f;
        float y = (id < 2) ? 0.5f : 0;
        playerCam.rect = new Rect(x, y, 0.5f, 0.5f);
    }

}
