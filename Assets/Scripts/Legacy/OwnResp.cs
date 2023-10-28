using Player;
using Signals;
using Zenject;
using UnityEngine;
using UnityEngine.UI;
using Infrastructure;
using Services.Factory;

public class OwnResp : MonoBehaviour, IFactorizable, IDamageable
{
	[Inject] private SignalBus _signalBus;
	[Inject] private IGameObjectsFactory _gameObjectsFactory;

	[SerializeField] private Image _healthBar;
	[SerializeField] private GameObject _explosionFX;
	
	private int _healthPoint = Constants.START_HP_OWNRESP;
	private float _currentHealth;

	public void Start()
	{
		_signalBus.Subscribe<GameRestartSignal>(RestartGame);
		
		_currentHealth = _healthPoint;
	}

	public void TakingDamage(int damage)
	{
		_healthPoint -= damage;
		_healthBar.fillAmount = _healthPoint / _currentHealth;
		if (_healthPoint <= 0) Termination();
	}

	private void Termination()
	{
		_signalBus.Fire<GameEndSignal>();
		_gameObjectsFactory.DestroyNonSingleGameObject(_gameObjectsFactory.InstantiateNonSingleGameObject(_explosionFX, transform.position, transform.rotation), 2.0f);
		_gameObjectsFactory.DestroySingleGameObject<OwnResp>();
	}
	
	private void RestartGame()
	{
		_healthPoint = Constants.START_HP_OWNRESP;
		_currentHealth = _healthPoint;
		_healthBar.fillAmount = _healthPoint / _currentHealth;
	}

	private void OnDestroy()
	{
		_signalBus.Unsubscribe<GameRestartSignal>(RestartGame);
	}
}