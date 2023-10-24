using System;
using System.Collections.Generic;
using Services;
using UnityEngine;

namespace Data
{
	[Serializable]
	public enum BuildType
	{
		Turret,
		PlasmaTurret,
		BigTurret
	}

	[Serializable]
	public class BuildDataModel
	{
		public BuildType type;
		public GameObject prefab;
		public int cost;
	}

	[CreateAssetMenu(fileName = "BuildData", menuName = "Data/BuildData")]
	public class BuildData : ScriptableObject
	{
		[SerializeField] private List<BuildDataModel> _buildDataModels = new();

		public GameObject GetItemByType(BuildType type)
		{
			return _buildDataModels.Find(item => item.type == type).prefab;
		}
		public T GetItemByType<T>(BuildType type) where T : IBuildable
		{
			return _buildDataModels.Find(item => item.type == type).prefab.GetComponent<T>();
		}

		public int GetItemCostByType(BuildType type)
		{
			return _buildDataModels.Find(item => item.type == type).cost;
		}
	}
}