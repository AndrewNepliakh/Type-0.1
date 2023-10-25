using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Services.Spawn
{
	[Serializable]
	public class Wave
	{
		public int waveCount;
		public GameObject prefab;
		public List<WayPoint> wayPoints = new();
	}

	[Serializable]
	public class WayPoint
	{
		public Vector3 position;
	}

	[CreateAssetMenu(fileName = "SpawnData", menuName = "Data/SpawnData")]
	public class SpawnData : ScriptableObject
	{
		[SerializeField] private List<Wave> _waves = new();

		public GameObject GetEnemyPrefab(int waveCount) => 
			_waves.Find(wave => wave.waveCount == waveCount).prefab;

		public Vector3 GetSpawnPoint(int waveCount) =>
			_waves.Find(wave => wave.waveCount == waveCount).wayPoints.First().position;

		public List<WayPoint> GetWay(int waveCount) =>
			_waves.Find(wave => wave.waveCount == waveCount).wayPoints.Skip(0).ToList();
	}
}