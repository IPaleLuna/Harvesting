using PaleLuna.Architecture.Services;
using Services;
using UnityEngine;

public class ButtonsActions : MonoBehaviour
{
    public void StartGame()
    {
        ServiceManager.Instance
            .GlobalServices.Get<SceneLoaderService>()
            .LoadScene(2);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
