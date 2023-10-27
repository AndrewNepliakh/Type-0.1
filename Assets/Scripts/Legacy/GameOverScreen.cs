using Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour {

    public void Retry()
    {
        SceneManager.LoadScene(Constants.GAME_SCENE);
    }

    public void Menu()
    {
        SceneManager.LoadScene(Constants.MENU_SCENE);
    }
}
