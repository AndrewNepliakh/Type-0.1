using System.Collections.Generic;
using Player;
using Services.Factory;
using UnityEngine;
using Services.Spawn;
using Signals;
using UnityEngine.UI;
using Zenject;

public class Enemy : MonoBehaviour, ISpawnable
{
	[Inject] private SignalBus _signalBus;
	[Inject] private IGameObjectsFactory _gameObjectsFactory;

	public float radius = 2f;
	public float force = 700f;
	public float healthPoint;
	public float speed = 10f;
	public GameObject explosionFX;
	public GameObject battery;
	public Image healthBar;

	private int damage = 10;
	private bool hasExploded;
	private PlayerHealth player;
	private OwnResp ownResp;
	private float currentHealth;
	private int waypointIndex = 0;
	private Vector3 wayPointTarget;
	private List<WayPoint> _wayPoints;

	private float _distanceToPlayer = 1.5f;
	private float _distanceToWayPoint = 0.2f;
	private float _distanceToOwnResp = 1.0f;

	public void Initialize(List<WayPoint> wayPoints)
	{
		_signalBus.Subscribe<GameEndSignal>(Termination);
		
		_wayPoints = wayPoints;
		wayPointTarget = _wayPoints[0].position;
		ownResp = _gameObjectsFactory.GetSingleGameObject<OwnResp>().GetComponent<OwnResp>();
		player = _gameObjectsFactory.GetSingleGameObject<PlayerController>().GetComponent<PlayerHealth>();
	}

	void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position, wayPointTarget, Time.deltaTime * speed);
		if (Vector3.Distance(transform.position, wayPointTarget) <= _distanceToWayPoint) GetNextWayPoint();

		if (player != null)
		{
			if (Vector3.Distance(transform.position, player.transform.position) <= _distanceToPlayer && !hasExploded)
			{
				player.TakingDamage(damage);
				Explode();
				hasExploded = true;
			}
		}
		else Termination();

		if (ownResp != null)
		{
			if (Vector3.Distance(transform.position, ownResp.transform.position) <= _distanceToOwnResp && !hasExploded)
			{
				ownResp.TakingDamage(damage);
				Explode();
				hasExploded = true;
			}
		}
		else Termination();
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

	public void IncreaseHealthRate(float value)
	{
		healthPoint += value;
		currentHealth = healthPoint;
	}

	private void GetNextWayPoint()
	{
		if (waypointIndex >= _wayPoints.Count - 1)
		{
			Destroy(gameObject);
			return;
		}

		waypointIndex++;
		wayPointTarget = _wayPoints[waypointIndex].position;
	}

	private void Explode()
	{
		Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);
		foreach (Collider closestObj in collidersToMove)
		{
			Rigidbody rb = closestObj.GetComponent<Rigidbody>();
			if (rb != null) rb.AddExplosionForce(force, transform.position, radius);
		}
		
		_gameObjectsFactory.DestroyNonSingleGameObject(_gameObjectsFactory.InstantiateNonSingleGameObject(explosionFX, transform.position, transform.rotation), 2.0f);
		Termination();
	}

	private void Termination()
	{
		if (!hasExploded)
		{
			_gameObjectsFactory.InstantiateNonSingleGameObject(battery, transform.position, Quaternion.identity);
			_gameObjectsFactory.DestroyNonSingleGameObject(gameObject);
		}

		hasExploded = true;
	}
}