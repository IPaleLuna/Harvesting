using PaleLuna.Architecture.Services;
using Services;
using UnityEngine;

public class ButtonsActions : MonoBehaviour
{
    [SerializeField]
    private BackstageScreen _startScreen;

    public void StartGame()
    {
        _startScreen.FadeOut(() =>
        {
            ServiceManager.Instance
            .GlobalServices.Get<SceneLoaderService>()
            .LoadScene("GameScene");
        });
    }
    public void Exit()
    {
        Application.Quit();
    }
}
