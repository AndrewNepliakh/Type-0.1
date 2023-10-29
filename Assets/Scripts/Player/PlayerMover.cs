using Services.InputService;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Player
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class PlayerMover : MonoBehaviour
	{
		[Inject] private IInputService _inputService;
		
		[SerializeField] private NavMeshAgent agent;
		private bool _isOtherHit;

		void Update()
		{
			if(_inputService.IsOverUI())return;
			
			if (Input.GetMouseButtonUp(0))
			{
				if (!_isOtherHit)
				{
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;

					if (Physics.Raycast(ray, out hit))
					{
						MoveToPoint(hit.point);
					}
				}

				_isOtherHit = false;
			}
		}

		private void MoveToPoint(Vector3 hitPoint)
		{
			agent.SetDestination(hitPoint);
		}
	}
}