using Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper : MonoBehaviour
{
    private async void Awake()
    {
        Application.targetFrameRate = 60;
        SceneManager.LoadScene(Constants.GAME_SCENE);
    }
}
