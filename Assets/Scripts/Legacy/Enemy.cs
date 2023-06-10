﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private int damage = 10;
    public float radius = 2f;
    public float detectingTargetRadius = 1f;
    public float force = 700f;
    public float healthPoint;
    private float currentHealth;
    public float speed = 10f;
    private int waypointIndex = 0;
    bool hasExploded = false;
    public GameObject explosionFX;
    public GameObject brokenEnemy;
    public GameObject battery;
    GameObject player;
    GameObject ownResp;
    public Image healthBar;
    Transform wayPointTarget;

    void Start()
    {
        player = GameObject.Find("Player");
        ownResp = GameObject.Find("OwnResp");
        GameObject masterControl = GameObject.FindGameObjectWithTag("MasterController");
        WaveSpawner enmHp = masterControl.GetComponent<WaveSpawner>();
        healthPoint = enmHp.enemyHp;
        currentHealth = healthPoint;
        wayPointTarget = WayPoints.points[0];
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, wayPointTarget.position, Time.deltaTime * speed);
        if (Vector3.Distance(transform.position, wayPointTarget.position) <= 0.2f) GetNextWaipoint();

        if (player != null )
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= 1.5f && !hasExploded)
            {
                Explode();
                hasExploded = true;

                Collider[] collidersToDamage = Physics.OverlapSphere(transform.position, radius);
                foreach (Collider nearObj in collidersToDamage)
                {
                    PlayerHealth dest = nearObj.GetComponent<PlayerHealth>();
                    if (dest != null)
                    {
                        if (dest.currentHealth <= damage) dest.Termination();
                        else dest.TakingDamage(damage);
                    }
                }

                Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);
                foreach (Collider closestObj in collidersToMove)
                {
                    Rigidbody rb = closestObj.GetComponent<Rigidbody>();
                    if (rb != null) rb.AddExplosionForce(force, transform.position, radius);
                }
            }
        }

        if (ownResp != null)
        {
            if (Vector3.Distance(transform.position, ownResp.transform.position) <= 1 && !hasExploded)
            {
                Damage(ownResp);
                Explode();
                hasExploded = true;
            }
        }
        else Termination();
    }

    void GetNextWaipoint()
    {
        if (waypointIndex >= WayPoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        waypointIndex++;
        wayPointTarget = WayPoints.points[waypointIndex];
    }

    void Explode()
    {
        Instantiate(explosionFX, transform.position, transform.rotation);
        Termination();
    }

    void Damage(GameObject obj)
    {
        if (obj == ownResp)
        {
            OwnResp ownRsp = obj.GetComponent<OwnResp>();
            ownRsp.TakingDamage(10);
        }
        if (obj == player)
        {
            PlayerHealth plrHp = obj.GetComponent<PlayerHealth>();
            plrHp.TakingDamage(10);
        }
    }

    void Termination()
    {
        if (!hasExploded)
        {
            Instantiate(battery, transform.position, Quaternion.identity);
            GameObject brokenEnemyInst = (GameObject)Instantiate(brokenEnemy, transform.position, transform.rotation);
            Destroy(brokenEnemyInst, 1.5f);
            Destroy(gameObject);
        }

        hasExploded = true;
    }

    public void TakeDamage(int amount)
    {
        healthPoint -= amount;
        healthBar.fillAmount = healthPoint / currentHealth;
        if (healthPoint <= 0)
        {
            Termination();
        }
    }
}
