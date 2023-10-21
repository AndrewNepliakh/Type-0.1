using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerControll : MonoBehaviour
{

    Camera cam;
    PlayerMotor motor;
    public LayerMask moveMask;
    public bool notUIhit;

    void Start()
    {
        notUIhit = false;
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (!notUIhit)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100, moveMask) && (hit.collider.name == "Stage" || hit.collider.name == "Ramp"))
                {
                    motor.MoveToPoint(hit.point);
                }
            }

            notUIhit = false;
        }
    }
}
