using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject levelScreenUI;
    public GameObject mainMenuUI;
    public GameObject load;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            levelScreenUI.SetActive(false);
            mainMenuUI.SetActive(true);
        }
    }

    public void Play()
    {
        //SceneManager.LoadScene("Sheep_test");
        load.GetComponent<LoadScreenControll>().LoadScreen();
    }

    public void Level()
    {
        levelScreenUI.SetActive(true);
        mainMenuUI.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
