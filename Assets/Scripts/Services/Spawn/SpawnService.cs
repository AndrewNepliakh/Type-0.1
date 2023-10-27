using Services.Factory;
using UnityEngine;
using Zenject;

namespace Services.Spawn
{
	public class SpawnService : ISpawnService
	{
		[Inject] private SpawnData _spawnData;
		[Inject] private IGameObjectsFactory _gameObjectsFactory;
		
		public T Spawn<T>(int wave) where T : ISpawnable
		{
			var spawnable =  _gameObjectsFactory.InstantiateObject(_spawnData.GetEnemyPrefab(wave),
				_spawnData.GetSpawnPoint(wave), Quaternion.identity).GetComponent<T>();
			spawnable.Initialize(_spawnData.GetWay(wave));
			return spawnable;
		}
		public float GetSpawnRepeating(int wave)
		{
			return _spawnData.GetSpawnRepeating(wave);
		}

		public float GetIncreaseHealthRate(int wave)
		{
			return _spawnData.GetIncreaseHealthRate(wave);
		}
	}
}