using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    GameObject ownResp;
    public Transform enemyPrefab;
    public Transform spawnPoint;
    //public Text waveCountText;
    public Text enemyHPText;
    public float delay = 3f;
    private float countDown = 2f;
    public int difficulty;
    int waveCount = 0;
    public float enemyHp;

    private void Start()
    {
        ownResp = GameObject.Find("OwnResp");
    }

    void Update()
    {
        if (waveCount != 0)
        {
            //waveCountText.text = waveCount.ToString();
            enemyHPText.text = enemyHp.ToString();
        }
        

        if (countDown <= 0f)
        {
            SpawnEnemy();
            countDown = delay;
            waveCount++;
            enemyHp += difficulty;
        }

        countDown -= Time.deltaTime;
    }

    void SpawnEnemy()
    {
        if (spawnPoint != null && ownResp != null) Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}

