using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using PaleLuna.Architecture.Services;
using Services;
using TMPro;
using UnityEngine;

public class SavePlayerScore : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _inputField;

    private PlayerScoreTable _scoreTable;

    private void Start()
    {
        _scoreTable = ServiceManager.Instance.GlobalServices.Get<PlayerScoreTable>();
    }

    public void CollectDataAndSave()
    {
        string playerName = _inputField.text;
        int maxScore = ServiceManager.Instance
            .GlobalServices
            .Get<SceneLoaderService>()
            .GetCurrentBaggage()
            .GetInt(StringKeys.MAX_SCORE_IN_SESSION);

        _ = ApplyDataEndExit(playerName, maxScore);
    }

    private async UniTaskVoid ApplyDataEndExit(string playerName, int score)
    {
        PlayerScore playerScore = new PlayerScore(playerName, score);

        if (await CheckScore(playerScore))
            await _scoreTable.Post(playerScore);

        ServiceManager.Instance
            .GlobalServices
            .Get<SceneLoaderService>()
            .LoadScene(1);
    }
    
    

    private async UniTask<bool> CheckScore(PlayerScore playerScore)
    {
        PlayerScore other = await _scoreTable.GetPlayerScore(playerScore.player_name);

        if (other.score < playerScore.score)
            return true;
        return false;
        
    }
}
