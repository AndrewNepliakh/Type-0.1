using Infrastructure;
using Player;
using Services.Factory;
using UnityEngine;
using Zenject;

public class Battery : MonoBehaviour
{
	[Inject] private IStorageService _storageService;
	[Inject] private IGameObjectsFactory _gameObjectsFactory;

	[SerializeField] private GameObject _ernedRatePrefab;
	[SerializeField] private GameObject _frustratePrefab;
	[SerializeField] private int _earning;

	private readonly float _destroyCount = 10f;
	private PlayerController _player;

	private void Start()
	{
		Invoke(nameof(Dissolve), _destroyCount);
	}

	private void Dissolve()
	{
		CancelInvoke();
		if (!_player) 
			_gameObjectsFactory.InstantiateNonSingleGameObject(_frustratePrefab, transform.position, Quaternion.identity);
		_gameObjectsFactory.DestroyNonSingleGameObject(gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.TryGetComponent(out _player)) return;
		_gameObjectsFactory.InstantiateNonSingleGameObject(_ernedRatePrefab, transform.position, Quaternion.identity);
		_storageService.AddEnergy(_earning);
		_gameObjectsFactory.DestroyNonSingleGameObject(gameObject);
	}
}