using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;
    bool paused;

    private void Start()
    {
        paused = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.gameIsOver == false && GameManager.lvlCompleted == false) Pause();
        }

    }

    public void Pause()
    {
        if (GameManager.gameIsOver == false && GameManager.lvlCompleted == false)
        {
            paused = !paused;
            pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);

            if (pauseMenuUI.activeSelf)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }

            GameObject[] turretBase = GameObject.FindGameObjectsWithTag("TurretBase");
            foreach (GameObject turBas in turretBase)
            {
                if (turBas != null)
                {
                    if (!paused) turBas.GetComponent<Collider>().enabled = false;
                    if (paused) turBas.GetComponent<Collider>().enabled = true;
                }
            }

            GameObject[] turret = GameObject.FindGameObjectsWithTag("Turret");
            foreach (GameObject tur in turret)
            {
                if (tur != null)
                {
                    if (!paused) tur.GetComponent<Collider>().enabled = false;
                    if (paused) tur.GetComponent<Collider>().enabled = true;
                }
            }
        }
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Munu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_menu");
    }

    public void Continue()
    {
        Pause();
    }
}
