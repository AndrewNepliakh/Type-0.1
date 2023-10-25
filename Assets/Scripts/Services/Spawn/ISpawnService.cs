namespace Services.Spawn
{
	public interface ISpawnService
	{
		T Spawn<T>(int wave) where T : ISpawnable;
	}
}