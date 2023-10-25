using Services.Factory;
using UnityEngine;

namespace Player
{
	[RequireComponent(typeof(PlayerMover))]
	public class PlayerController : MonoBehaviour, IFactorizable
	{
		public void Start()
		{
		
		}
	}
}