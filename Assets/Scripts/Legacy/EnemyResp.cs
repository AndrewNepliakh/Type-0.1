using Services.Factory;
using UnityEngine.UI;
using Infrastructure;
using UnityEngine;
using Signals;
using Zenject;

public class EnemyResp : MonoBehaviour, IFactorizable
{
	[Inject] private SignalBus _signalBus;
	[Inject] private IGameObjectsFactory _gameObjectsFactory;

	public GameObject explosionFX;
	public GameObject brokenResp;
	public Image healthBar;

	private float _currentHealth;
	private bool _hasExploded;
	private int _healthPoint;

	public void Start()
	{
		RestartGame();
		_signalBus.Subscribe<GameLateRestartSignal>(RestartGame);
	}

	private void RestartGame()
	{
		_healthPoint = Constants.START_HP_ENEMY_RESP;
		_currentHealth = _healthPoint;
		healthBar.fillAmount = _healthPoint / _currentHealth;
	}


	public void TakeDamage(int amount)
	{
		_healthPoint -= amount;
		healthBar.fillAmount = _healthPoint / _currentHealth;
		if (_healthPoint <= 0 && !_hasExploded)
		{
			_hasExploded = true;
			Termination();
		}
	}

	void Termination()
	{
		_gameObjectsFactory.DestroyNonSingleGameObject(
			_gameObjectsFactory.InstantiateNonSingleGameObject(explosionFX, transform.position, transform.rotation), 2);
		_gameObjectsFactory.DestroyNonSingleGameObject(
			_gameObjectsFactory.InstantiateNonSingleGameObject(brokenResp, transform.position, transform.rotation), 2);
		_gameObjectsFactory.DestroySingleGameObject<EnemyResp>();
		_signalBus.Fire<LevelCompleteSignal>();
	}

	private void OnDestroy()
	{
		_signalBus.Unsubscribe<GameLateRestartSignal>(RestartGame);
	}
}