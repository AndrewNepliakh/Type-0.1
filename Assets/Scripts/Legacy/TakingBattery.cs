using Infrastructure;
using Player;
using UnityEngine;
using Zenject;

public class TakingBattery : MonoBehaviour
{
	[Inject] private IStorageService _storageService;

	[SerializeField] private GameObject _ernedRatePrefab;
	[SerializeField] private GameObject _frustratePrefab;
	[SerializeField] private int _earning;

	private readonly float _destroyCount = 10f;
	private PlayerController _player;

	private void Start()
	{
		Destroy(gameObject, _destroyCount);
	}

	private void OnDestroy()
	{
		Dissolve();
	}

	private void Dissolve()
	{
		if (!_player) 
			Instantiate(_frustratePrefab, transform.position, Quaternion.identity);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.TryGetComponent(out _player)) return;
		Instantiate(_ernedRatePrefab, transform.position, Quaternion.identity);
		_storageService.AddEnergy(_earning);
		Destroy(gameObject);
	}
}