using PaleLuna.Architecture.Services;
using Services;
using UnityEngine;

public class ButtonsActions : MonoBehaviour
{
    [SerializeField]
    private BackstageScreen _startScreen;
    [SerializeField]
    private BackgrounMusic _backgrounMusic;

    public void StartGame()
    {
        _backgrounMusic.SmoothStop();
        
        _startScreen.FadeOut(() =>
        {
            ServiceManager.Instance
            .GlobalServices.Get<SceneLoaderService>()
            .LoadScene("GameScene");
        });
    }
}
