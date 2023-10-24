using Unity.VisualScripting;

namespace Infrastructure
{
	public interface IStorageService
	{
		public int Energy{get; }
		public int Mines{get; }
		void AddEnergy(int value);
		void SubtractEnergy(int value);
	}
}