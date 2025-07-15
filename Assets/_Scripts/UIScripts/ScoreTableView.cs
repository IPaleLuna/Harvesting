using Cysharp.Threading.Tasks;
using Services;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTableView : MonoBehaviour
{
    [SerializeField]
    private ScoreCardView _scoreCardPrefab;

    private PlayerScoreTable _scoreTable;
    private List<ScoreCardView> _cards;

    private void Awake()
    {
        _scoreTable = ServiceManager.Instance.GlobalServices.Get<PlayerScoreTable>();

        ScoreCardView[] cardList = transform.GetComponentsInChildren<ScoreCardView>();
        _cards = new(cardList);
        DisableAll();
    }

    private async UniTaskVoid Refresh()
    {
        DisableAll();
        List<PlayerScore> response = await _scoreTable.GetPlayerScores();

        for(int i = 0; i < response.Count; i++)
        {
            if (i >= _cards.Count)
                _cards.Add(Instantiate(_scoreCardPrefab, this.transform));

            _cards[i].UpdateCard(i + 1, response[i].player_name, response[i].score);
            _cards[i].gameObject.SetActive(true);
        }
    }

    private void DisableAll() =>
        _cards.ForEach(item => item.gameObject.SetActive(false));


    private void OnEnable()
    {
        _ = Refresh();
    }
}
