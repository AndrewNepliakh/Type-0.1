using System;
using Zenject;

namespace Infrastructure
{
	public class StorageService : IStorageService, IInitializable
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

		public Action<int> OnEnergyChanged { get; set; }
		public Action<int> OnMinesChanged { get; set; }
		
		public void Initialize()
		{
			_energy = Constants.START_ENERGY;
		}

		public void AddEnergy(int value)
		{
			_energy += value;
			OnEnergyChanged?.Invoke(_energy);
		}

		public void SubtractEnergy(int value)
		{
			_energy -= value;
			OnEnergyChanged?.Invoke(_energy);
		}
		
		public void AddMines(int value)
		{
			_mines += value;
			OnMinesChanged?.Invoke(_mines);
		}

		public void SubtractMines(int value)
		{
			_mines -= value;
			OnMinesChanged?.Invoke(_mines);
		}

		
	}
}