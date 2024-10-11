using Services;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameEndScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _winText;
    [SerializeField]
    private GameObject _hitGo;


    private void Start()
    {
        GameEvents.timeOutEvent.AddListener(OnTimeOut);
    }

    private void OnTimeOut()
    {
        Player winPlayer = ServiceManager.Instance.LocalServices.Get<ScoreHolder>().playerWithMaxScore;

        _winText.SetText($"Player {winPlayer.playerID + 1} win!");

        _winText.gameObject.SetActive(true);
        _hitGo.SetActive(true);
    }
}
