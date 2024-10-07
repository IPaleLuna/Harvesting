using UnityEngine;

public class SetPlayerCountMode : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.PLAYER_COUNT_KEY, (int)PlayerCount.Two);
    }
    public void OnModeChange(IOptionalData<int> data)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.PLAYER_COUNT_KEY, data.GetData(PlayerPrefsKeys.PLAYER_COUNT_KEY));
    }
}
