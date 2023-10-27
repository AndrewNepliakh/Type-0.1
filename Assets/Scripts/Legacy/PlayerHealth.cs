using Player;
using Signals;
using UnityEngine;
using Zenject;

public class PlayerHealth : MonoBehaviour, IDamageable
{
	[Inject] private SignalBus _signalBus;
	
	public int healthPoint = 50;
	public int currentHealth;
	public GameObject deadSheep;
	public Vector3 offset;

	void Start()
	{
		currentHealth = healthPoint;
	}

	void Update()
	{
		if (currentHealth <= 0)
		{
			Termination();
		}
	}

	public void TakingDamage(int damage)
	{
		currentHealth -= damage;
	}

	public void Termination()
	{
		_signalBus.Fire<GameEndSignal>();
		Instantiate(deadSheep, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}