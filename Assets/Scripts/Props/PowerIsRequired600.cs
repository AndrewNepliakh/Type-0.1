using Services.Factory;
using UnityEngine;
using Zenject;

namespace Services.UI
{
	public class PowerIsRequired600 : MonoBehaviour, IFactorizable
	{
		[Inject] private IGameObjectsFactory _gameObjectsFactory;
		public void Start()
		{
			_gameObjectsFactory.DestroySingleGameObject<PowerIsRequired300>(2);
		}
	}
}