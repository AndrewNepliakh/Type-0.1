using UnityEngine;

public class Turret : MonoBehaviour
{
	public Transform target;
	public float range = 15f;
	public Transform partToRotate;
	public string enemyTag = "Enemy";
	public float turnSpeed = 10f;
	public float fireRate = 1f;
	private float fireCountdown = 0f;
	public GameObject bulletPrefab;
	public Transform firePoint;
	public Transform lamp;
	public Transform shootSplash;
	public Transform cartridgeEmit;
	bool setActive;

	void Start()
	{
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
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

		if (nearestEnemy != null && shortestDistance > range)
		{
			target = null;
		}
	}

	void Update()
	{
		if (target == null)
		{
			var shtFire = shootSplash.GetComponent<ParticleSystem>().emission;
			shtFire.enabled = false;
			var cart = cartridgeEmit.GetComponent<ParticleSystem>().emission;
			cart.enabled = false;
			lamp.GetComponent<Light>().color = Color.green;
			return;
		}


		lamp.GetComponent<Light>().color = Color.red;

		var _shtFire = shootSplash.GetComponent<ParticleSystem>().emission;
		_shtFire.enabled = true;
		var _emit = cartridgeEmit.GetComponent<ParticleSystem>().emission;
		_emit.enabled = true;

		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

		if (fireCountdown <= 0f)
		{
			Shoot();
			fireCountdown = 1f / fireRate;
		}

		fireCountdown -= Time.deltaTime;
	}

	void Shoot()
	{
		GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();
		if (bullet != null) bullet.Seek(target.transform);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}