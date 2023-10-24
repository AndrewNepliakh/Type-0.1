using System;
using Unity.VisualScripting;

namespace Infrastructure
{
	public interface IStorageService
	{
		public int Energy{get; }
		public int Mines{get; }
		Action<int> OnEnergyChanged { get; set; }
		Action<int> OnMinesChanged { get; set; }
		void AddEnergy(int value);
		void SubtractEnergy(int value);
	}
}