﻿using UnityEngine;

public class ExplosionMine : MonoBehaviour
{
	public float radius = 3f;
	public float force = 700f;
	public float range = 1f;
	bool hasExploded = false;
	public GameObject explosionFX;
	Transform target;

	void Start()
	{
		InvokeRepeating("UpdateTarget", 0f, 0.1f);
	}

	void Update()
	{
		if (target == null)
		{
			return;
		}
		else if (!hasExploded)
		{
			Damage(target);
			Explode();
			hasExploded = true;
		}
	}

	void Explode()
	{
		var explosion = Instantiate(explosionFX, transform.position, transform.rotation);
		explosion.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
		Destroy(explosion, 2);

		Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
		foreach (Collider closestObj in colliders)
		{
			Rigidbody rb = closestObj.GetComponent<Rigidbody>();
			if (rb != null) rb.AddExplosionForce(force, transform.position, radius);
		}

		Destroy(gameObject);
	}

	void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
		}
	}

	void Damage(Transform enemy)
	{
		Enemy target = enemy.GetComponent<Enemy>();
		if (target != null)
		{
			target.TakeDamage(50);
		}
	}
}