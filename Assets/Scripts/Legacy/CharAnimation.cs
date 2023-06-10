using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharAnimation : MonoBehaviour {

    const float animSmoothTime = .1f;
    public VirtualJoystick _moveForward;

    Animator anim;
    NavMeshAgent agent;

	void Start () {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
	}
	
	void Update () {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("speedPercent", speedPercent, animSmoothTime, Time.deltaTime);
       // if (_moveForward.Vertical() != 0.0f || _moveForward.Vertical() != 0.0f) anim.SetFloat("speedPercent", 1);
	}
}
