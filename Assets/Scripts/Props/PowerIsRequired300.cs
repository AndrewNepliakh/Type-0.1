using Services.Factory;
using UnityEngine;
using Zenject;

namespace Services.UI
{
	public class PowerIsRequired300 : MonoBehaviour, IFactorizable
	{
		[Inject] private IGameObjectsFactory _gameObjectsFactory;
		void Start()
		{
			_gameObjectsFactory.DestroySingleGameObject<PowerIsRequired300>(2);
		}
	}
}