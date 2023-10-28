using Infrastructure;
using Signals;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameOverScreen : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;
    
    public void Retry()
    {
        _signalBus.Fire<GameRestartSignal>();
        SceneManager.LoadScene(Constants.GAME_SCENE);
    }

    public void Menu()
    {
        SceneManager.LoadScene(Constants.MENU_SCENE);
    }
}
