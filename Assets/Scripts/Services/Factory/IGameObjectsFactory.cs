using UnityEngine;

namespace Services.Factory
{
	public interface IGameObjectsFactory
	{
		public GameObject InstantiateSingleGameObject<T>(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null) where T : IFactorizable;
		public GameObject InstantiateObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null);
		public void DestroySingleGameObject<T>(int delay = 0) where T : IFactorizable;
		public GameObject GetGameObject<T>() where T : IFactorizable;
	}
}