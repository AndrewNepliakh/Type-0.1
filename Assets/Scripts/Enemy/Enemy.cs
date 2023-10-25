using System.Collections.Generic;
using Player;
using Services.Factory;
using UnityEngine;
using Services.Spawn;
using UnityEngine.UI;
using Zenject;

public class Enemy : MonoBehaviour, ISpawnable
{
    [Inject] private IGameObjectsFactory _gameObjectsFactory;
    
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
    private GameObject player;
    private GameObject ownResp;
    public Image healthBar;
    private Vector3 wayPointTarget;
    private List<WayPoint> _wayPoints;

    public void Initialize(List<WayPoint> wayPoints)
    {
        _wayPoints = wayPoints;
        wayPointTarget = _wayPoints[0].position;
        
        currentHealth = healthPoint;

        ownResp = _gameObjectsFactory.GetGameObject<OwnResp>();
        player = _gameObjectsFactory.GetGameObject<PlayerController>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, wayPointTarget, Time.deltaTime * speed);
        if (Vector3.Distance(transform.position, wayPointTarget) <= 0.2f) GetNextWayPoint();

        if (player != null)
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

        if (_gameObjectsFactory.GetGameObject<EnemyResp>() != null)
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

    void GetNextWayPoint()
    {
        if (waypointIndex >= _wayPoints.Count - 1)
        {
            Destroy(gameObject);
            return;
        }
        waypointIndex++;
        wayPointTarget = _wayPoints[waypointIndex].position;
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
