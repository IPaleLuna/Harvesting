using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;


public class LobbySearcher : Searcher<Lobby>
{
    public override async UniTask<List<Lobby>> Search()
    {
        try
        {
            QueryResponse response = await LobbyService.Instance.QueryLobbiesAsync(new QueryLobbiesOptions
            {
                Filters = new List<QueryFilter>
                {
                    new QueryFilter(
                        field: QueryFilter.FieldOptions.AvailableSlots,
                        op: QueryFilter.OpOptions.GT,
                        value: "0") // Лобби с доступными слотами
                },
                Order = new List<QueryOrder> {
                new (false, QueryOrder.FieldOptions.Created)
            }
            });
            
            return response.Results;
        }
        catch (LobbyServiceException  e)
        {
            Debug.LogError($"Ошибка получения списка лобби: {e.Message}");
        }
        
        return null;
    }
}
