using Services.Factory;
using UnityEngine;

public class UltimateGun : MonoBehaviour, IFactorizable
{
	public GameObject target;
	public float range = 15f;
	public Transform partToRotate;
	public string enemyTag = "Enemy";
	public float turnSpeed = 10f;
	public float fireRate = 1f;
	private float fireCountdown = 0f;
	public GameObject bulletPrefab;
	public Transform firePoint;

	void Awake()
	{
		GetComponent<UltimateGun>().enabled = false;
	}

	public void Start()
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
			target = nearestEnemy;
		}
	}

	void Update()
	{
		if (target == null)
		{
			return;
		}

		Vector3 dir = target.transform.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);

		if (fireCountdown <= 0f)
		{
			Shoot();
			fireCountdown = 1f / fireRate;
		}

		fireCountdown -= Time.deltaTime;
	}

	void Shoot()
	{
		GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		UltimateBullet bullet = bulletGO.GetComponent<UltimateBullet>();
		if (bullet != null) bullet.Seek(target.transform);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}