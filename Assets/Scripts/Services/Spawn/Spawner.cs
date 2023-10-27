using UnityEngine;
using Zenject;

namespace Services.Spawn
{
	public class Spawner : MonoBehaviour
	{
		[Inject] protected ISpawnService _spawnService;
		private float _spawnRepeating;
		private int _waveCount = 1;
		private float _health;

		private void Start()
		{
			_spawnRepeating = _spawnService.GetSpawnRepeating(_waveCount);
			InvokeRepeating(nameof(Spawn), 5, _spawnRepeating);
		}

		private void Spawn()
		{
			_health += _spawnService.GetIncreaseHealthRate(_waveCount);
			_spawnService.Spawn<Enemy>(_waveCount).IncreaseHealthRate(_health);
		}
	}
}