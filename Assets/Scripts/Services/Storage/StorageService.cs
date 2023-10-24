namespace Infrastructure
{
	public class StorageService : IStorageService
	{
		private int _energy;
		private int _mines;
		
		public int Energy
		{
			get => _energy > 0 ? _energy : 0;
			private set => _energy = value > 0 ? value : 0;
		}

		public int Mines
		{
			get => _mines > 0 ? _mines : 0;
			private set => _mines = value > 0 ? value : 0;
		}

		public void AddEnergy(int value) => _energy += value;

		public void SubtractEnergy(int value) => _energy -= value;
	}
}