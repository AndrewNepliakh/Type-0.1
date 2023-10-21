using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour {

    NavMeshAgent agent;
    Vector3 vectPoint;
    public float speed;
    public float turnSmoothTime = 1.0f;
    float turnSmoothVel;
    public VirtualJoystick moveForward;

	void Start () {
        agent = GetComponent<NavMeshAgent>();

	}

    private void Update()
    {
       /* Vector2 input = new Vector2(moveForward.Horizontal(), moveForward.Vertical());
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVel, turnSmoothTime);
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        }*/

    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }
}
