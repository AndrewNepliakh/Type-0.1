using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static bool gameIsOver;
    public static bool lvlCompleted;
    public GameObject gameOverUi;
    public GameObject LvlCompleteUi;

    void Start()
    {
        gameIsOver = false;
        lvlCompleted = false;
    }

    void Update ()
    {

        // if (gameIsOver) return;
        // GameObject player = GameObject.Find("Player");
        // GameObject ownResp = GameObject.Find("OwnResp");
        // GameObject enemyResp = GameObject.Find("EnemyResp");
        // if (player == null || ownResp == null) GameOver();
        // if (enemyResp == null) LevelCompleted();

    }

    void GameOver()
    {
        gameIsOver = true;
        gameOverUi.SetActive(!gameOverUi.activeSelf);

        GameObject[] turretBase = GameObject.FindGameObjectsWithTag("TurretBase");
        foreach (GameObject turBas in turretBase)
        {
            if (turBas != null)
            {
                turBas.GetComponent<Collider>().enabled = false;
            }
        }

        GameObject[] turret = GameObject.FindGameObjectsWithTag("Turret");
        foreach (GameObject tur in turret)
        {
            if (tur != null)
            {
               tur.GetComponent<Collider>().enabled = false;
            }
        } 

        GameObject player = GameObject.Find("Player");
        if (player != null) player.GetComponent<PlayerController>().enabled = false;

    }

    public void LevelCompleted()
    {
        lvlCompleted = true;
        LvlCompleteUi.SetActive(true);

        GameObject[] turretBase = GameObject.FindGameObjectsWithTag("TurretBase");
        foreach (GameObject turBas in turretBase)
        {
            if (turBas != null)
            {
               turBas.GetComponent<Collider>().enabled = false;
            }
        }

        GameObject[] turret = GameObject.FindGameObjectsWithTag("Turret");
        foreach (GameObject tur in turret)
        {
            if (tur != null)
            {
               tur.GetComponent<Collider>().enabled = false;
            }
        }

        GameObject player = GameObject.Find("Player");
        if (player != null) player.GetComponent<PlayerController>().enabled = false;
    }
}
