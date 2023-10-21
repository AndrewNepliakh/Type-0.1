using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Player
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class PlayerMover : MonoBehaviour
	{
		[SerializeField] private NavMeshAgent agent;
		
		private LayerMask _moveMask;
		private bool _isUIHit;

		void Update()
		{
			if (Input.GetMouseButtonUp(0))
			{
				if (!_isUIHit)
				{
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;

					if (Physics.Raycast(ray, out hit))
					{
						MoveToPoint(hit.point);
					}
				}

				_isUIHit = false;
			}
		}

		private void MoveToPoint(Vector3 hitPoint)
		{
			agent.SetDestination(hitPoint);
		}
	}
}