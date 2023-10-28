using Player;
using UnityEngine;

namespace Services.Factory
{
	public interface IGameObjectsFactory
	{
		public GameObject InstantiateSingleGameObject<T>(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null) where T : IFactorizable;
		public GameObject InstantiateNonSingleGameObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null);
		public void DestroySingleGameObject<T>(float delay = 0) where T : IFactorizable;
		public void DestroyNonSingleGameObject(GameObject go, float delay = 0);
		public GameObject GetSingleGameObject<T>() where T : IFactorizable;
		public IDamageable[] GetDamageables();
		void DestroyAllNonSingleObjects();
	}
}