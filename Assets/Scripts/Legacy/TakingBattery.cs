using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakingBattery : MonoBehaviour
{
    public float range;
    float destroyCount = 9.9f;
    public GameObject ernedRate;
    public GameObject frustrate;
    public int earning;

    void Start()
    {
        Destroy(gameObject, 10);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void Update()
    {
        destroyCount -= Time.deltaTime;
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            float playerDist = Vector3.Distance(transform.position, player.transform.position);


            if (destroyCount <= 0)
            {
                Instantiate(frustrate, transform.position, Quaternion.identity);
                destroyCount = 9.9f;
            }

            if (playerDist <= range)
            {
                Instantiate(ernedRate, transform.position, Quaternion.identity);
                player.GetComponent<Earning>().earning += 100;
                Destroy(gameObject);
            }
        }

    }

}

