using System;
using System.Collections.Generic;
using System.Linq;
using Player;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Services.Factory
{
	public class GameObjectsFactory : IGameObjectsFactory
	{
		[Inject] private DiContainer _diContainer;
		private Dictionary<Type, GameObject> _singleGameObjects = new();
		private List<GameObject> _nonSingleObjects = new();

		public GameObject InstantiateSingleGameObject<T>(GameObject prefab, Vector3 position, Quaternion rotation,
			Transform parent = null) where T : IFactorizable
		{
			if (_singleGameObjects.TryGetValue(typeof(T), out var value))
			{
				return value;
			}
			else
			{
				GameObject go = _diContainer.InstantiatePrefab(prefab, position, rotation, parent);
				_singleGameObjects.Add(typeof(T), go);
				return go;
			}
		}

		public GameObject InstantiateNonSingleGameObject(GameObject prefab, Vector3 position, Quaternion rotation,
			Transform parent = null)
		{
			var obj = _diContainer.InstantiatePrefab(prefab, position, rotation, parent);
			_nonSingleObjects.Add(obj);
			return obj;
		}

		public void DestroySingleGameObject<T>(float delay = 0) where T : IFactorizable
		{
			if (_singleGameObjects.TryGetValue(typeof(T), out var value))
			{
				_singleGameObjects.Remove(typeof(T));
				Object.Destroy(value, delay);
			}
		}
		
		public void DestroyNonSingleGameObject(GameObject go, float delay = 0)
		{
			if (_nonSingleObjects.Contains(go))
			{
				_nonSingleObjects.Remove(go);
				Object.Destroy(go, delay);
			}
		}

		public GameObject GetSingleGameObject<T>() where T : IFactorizable
		{
			return _singleGameObjects.TryGetValue(typeof(T), out var value) ? value : null;
		}

		public IDamageable[] GetDamageables()
		{
			var damageables = new List<IDamageable>();
			
			foreach (var go in _singleGameObjects.Values)
			{
				if (go.TryGetComponent<IDamageable>(out var idamageable))
				{
					damageables.Add(idamageable);
				}
			}

			return damageables.ToArray();
		}

		public void DestroyAllNonSingleObjects()
		{
			for (var i = _nonSingleObjects.Count - 1; i >= 0; i--)
			{
				Object.Destroy(_nonSingleObjects[i].gameObject);
			}
			
			_nonSingleObjects.Clear();
		}
	}
}