using Unity.VisualScripting;

namespace Infrastructure
{
	public interface IStorageService
	{
		public int Energy{get; set;}
		public int Mines{get; set;}
	}
}