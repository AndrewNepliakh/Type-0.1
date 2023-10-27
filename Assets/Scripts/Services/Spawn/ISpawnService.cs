namespace Services.Spawn
{
	public interface ISpawnService
	{
		T Spawn<T>(int wave) where T : ISpawnable;

		float GetSpawnRepeating(int wave);
		float GetIncreaseHealthRate(int wave);
	}
}