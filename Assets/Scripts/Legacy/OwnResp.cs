using Services.Factory;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class OwnResp : MonoBehaviour, IFactorizable
{
	[Inject] private IGameObjectsFactory _gameObjectsFactory;

	public int healthPoint = 50;
	private float currentHealth;
	public Image healthBar;
	public GameObject explosionFX;

	public void Start()
	{
		currentHealth = healthPoint;
	}

	void Update()
	{
		healthBar.fillAmount = healthPoint / currentHealth;
		if (healthPoint <= 0)
		{
			Termination();
		}
	}

	public void TakingDamage(int damage)
	{
		healthPoint -= damage;
	}

	void Termination()
	{
		Destroy(Instantiate(explosionFX, transform.position, transform.rotation), 2.0f);
		_gameObjectsFactory.DestroySingleGameObject<OwnResp>();
	}
}