using Signals;
using UnityEngine;
using Zenject;

namespace Services.Spawn
{
	public class Spawner : MonoBehaviour
	{
		[Inject] private SignalBus _signalBus;
		[Inject] private ISpawnService _spawnService;
		
		private float _spawnRepeating;
		private int _waveCount = 1;
		private float _health ;
		private float _startDelay = 5.0f;

		private void Start()
		{
			_signalBus.Subscribe<GameEndSignal>(OnGameEnd);
			_signalBus.Subscribe<GameLateRestartSignal>(OnGameRestart);
			
			InitiateSpawner();
		}

		private void InitiateSpawner()
		{
			_spawnRepeating = _spawnService.GetSpawnRepeating(_waveCount);
			InvokeRepeating(nameof(Spawn), _startDelay, _spawnRepeating);
		}

		private void Spawn()
		{
			_health += _spawnService.GetIncreaseHealthRate(_waveCount);
			_spawnService.Spawn<Enemy>(_waveCount).IncreaseHealthRate(_health);
		}
		
		private void OnGameRestart() => InitiateSpawner();

		private void OnGameEnd() => CancelInvoke();

		private void OnDestroy()
		{
			_signalBus.Unsubscribe<GameEndSignal>(OnGameEnd);
			_signalBus.Unsubscribe<GameLateRestartSignal>(OnGameRestart);
		}
	}
}