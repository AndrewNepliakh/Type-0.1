using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Services.Factory
{
	public class GameObjectsFactory : IGameObjectsFactory
	{
		[Inject] private DiContainer _diContainer;
		private Dictionary<Type, GameObject> _gameObjects = new();

		public GameObject InstantiateSingleGameObject<T>(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null) where T : IFactorizable
		{
			if (_gameObjects.TryGetValue(typeof(T), out var value))
			{
				return value;
			}
			else
			{
				GameObject go = _diContainer.InstantiatePrefab(prefab, position, rotation, parent);
				_gameObjects.Add(typeof(T), go);
				return go;
			}
		}

		public GameObject InstantiateObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
		{
			return _diContainer.InstantiatePrefab(prefab, position, rotation, parent);
		}

		public void DestroySingleGameObject<T>(int delay = 0) where T : IFactorizable
		{
			if (_gameObjects.TryGetValue(typeof(T), out var value))
			{
				_gameObjects.Remove(typeof(T));
				UnityEngine.Object.Destroy(value, delay);
			}
		}
		
		public GameObject GetGameObject<T>() where T : IFactorizable
		{
			return _gameObjects.TryGetValue(typeof(T), out var value) ? value : null;
		}
	}
}