using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Player
{
	public class PlayerAnimation : MonoBehaviour
	{
		[SerializeField] private Animator _animator;
		[SerializeField] private NavMeshAgent _agent;

		private readonly string RunAnimation = "IsRun";

		private void Update()
		{
			switch (_agent.velocity.magnitude)
			{
				case > 0.1f:
					_animator.SetBool(RunAnimation, true);
					break;
				case < 0.1f:
					_animator.SetBool(RunAnimation, false);
					break;
			}
		}
	}
}