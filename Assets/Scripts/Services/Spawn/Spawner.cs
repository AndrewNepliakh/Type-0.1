using UnityEngine;
using Zenject;

namespace Services.Spawn
{
	public class Spawner : MonoBehaviour
	{
		[Inject] protected ISpawnService _spawnService;

		private void Start()
		{
			InvokeRepeating(nameof(Spawn), 2, 2);
		}

		private void Spawn()
		{
			_spawnService.Spawn<Enemy>(1);
		}
	}
}