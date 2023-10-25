using Data;
using UnityEngine;
using Zenject;

namespace Services
{
	public class BuildService : IBuildService
	{
		[Inject] private BuildData _buildData;

		public GameObject GetItemToBuild(BuildType type)
		{
			return _buildData.GetItemByType(type);
		}

		public T GetItemToBuild<T>(BuildType type) where T : IBuildable
		{
			return _buildData.GetItemByType<T>(type);
		}

		public int GetBuildItemCost(BuildType type)
		{
			return _buildData.GetItemCostByType(type);
		}
	}
}