using Services.Factory;
using UnityEngine;
using UnityEngine.UI;

public class EnemyResp : MonoBehaviour, IFactorizable
{
	public float force = 700f;
	public float healthPoint = 100;
	private float currentHealth;
	bool hasExploded = false;
	public GameObject explosionFX;
	public GameObject brokenResp;
	public Image healthBar;

	public void Start()
	{
		currentHealth = healthPoint;
	}

	private void Update()
	{
		healthBar.fillAmount = healthPoint / currentHealth;
	}

	public void TakeDamage(int amount)
	{
		healthPoint -= amount;
		if (healthPoint <= 0 && !hasExploded)
		{
			hasExploded = true;
			Termination();
		}
	}

	void Termination()
	{
		Destroy(Instantiate(explosionFX, transform.position, transform.rotation), 2);
		Destroy(Instantiate(brokenResp, transform.position, transform.rotation), 2);
		Destroy(gameObject);
	}
}