using Cysharp.Threading.Tasks;
using PaleLuna.Architecture.Services;
using PaleLuna.httpRequests;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerScoreTable: IService
{
    private const string SCORE_KEY = "score_url";

    private HttpUrls _urls;

    public PlayerScoreTable(HttpUrls urls)
    {
        this._urls = urls;
    }

    public async UniTask<List<PlayerScore>> GetPlayerScores()
    {
        string result = await HttpRequestSender.GetSend(_urls.urlsDic[SCORE_KEY]);

        List<PlayerScore> response = JsonConvert.DeserializeObject<List<PlayerScore>>(result);

        _ = GetPlayerScore(123);

        return response;
    }
    public async UniTask<PlayerScore> GetPlayerScore(int id)
    {
        string result_url = _urls.urlsDic[SCORE_KEY] + $"/{id}";
        string response = await HttpRequestSender.GetSend(result_url);

        List<PlayerScore> playerScore = JsonConvert.DeserializeObject<List<PlayerScore>>(response);

        if (playerScore.Count == 0)
            return new PlayerScore("", 0);
        return playerScore[0];
    }

    public async UniTask<PlayerScore> GetPlayerScore(string name)
    {
        string result_url = _urls.urlsDic[SCORE_KEY] + $"/name/{name}";
        string response = await HttpRequestSender.GetSend(result_url);

        List<PlayerScore> playerScore = JsonConvert.DeserializeObject<List<PlayerScore>>(response);

        if (playerScore.Count == 0)
            return new PlayerScore("", 0);

        return playerScore[0];
    }

    public async UniTask Post(PlayerScore model)
    {
        Debug.Log(model.player_name);
        string playerDataJson = JsonConvert.SerializeObject(model);
        Debug.Log(playerDataJson);

        await HttpRequestSender.PostSend(_urls.urlsDic[SCORE_KEY], playerDataJson);
    }
}


