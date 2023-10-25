using System.Collections.Generic;

namespace Services.Spawn
{
	public interface ISpawnable
	{
		void Initialize(List<WayPoint> wayPoints);
	}
}