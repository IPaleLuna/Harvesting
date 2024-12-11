using System;
using Harvesting.Collectable.Apple;
using Unity.Netcode;
using UnityEngine;

public class TestAppleSpawn : NetworkBehaviour
{
    [SerializeField]
    private NetworkAppleHandler applePrefab;
    
    [SerializeField]
    private Vector3 spawnPosition;
    
    [SerializeField]
    private NetworkAppleHandler existingApple;

    private void Update()
    {
        if(!IsServer) return;
        
        if(Input.GetKeyDown(KeyCode.Space))
            MoveApple();
    }

    private void SpawnApple()
    {
        NetworkAppleHandler apple = Instantiate(applePrefab);
        
        apple.RespawnApple(spawnPosition);
    }

    private void MoveApple()
    {
        existingApple.RespawnApple(spawnPosition);
    }
    
}
