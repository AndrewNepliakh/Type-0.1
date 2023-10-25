using Data;
using UnityEngine;

namespace Services
{
	public interface IBuildService
	{
		GameObject GetItemToBuild(BuildType type);
		T GetItemToBuild<T>(BuildType type) where T : IBuildable;
		int GetBuildItemCost(BuildType type);
	}
}