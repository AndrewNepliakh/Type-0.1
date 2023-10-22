﻿using UnityEngine;

namespace Services.Factory
{
	public interface IGameObjectsFactory
	{
		public GameObject InstantiateSingleGameObject<T>(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null) where T : IFactorizable;
		public void DestroySingleGameObject<T>(int delay = 0) where T : IFactorizable;
		public GameObject GetGameObject<T>(T type) where T : IFactorizable;
	}
}