using UnityEngine;
using UnityEngine.UI;

public class SwipeManager : MonoBehaviour
{
    private Transform cameraHandle;
    private Camera mainCamera;
    private Quaternion _desiredRotation;

    private Vector3 firstTouchPrevPos;
    private Vector3 secondTouchPrevPos;

    private float touchesPrevPosDifference;
    private float touchesCurPosDifference;

    private float zoomModifier;
    private float zoomModifierSpeed = 1.0f;

    private float speed = 6.0f;
    private float gravity = -9.8f;

    private void Start()
    {
        mainCamera = Camera.main;
        cameraHandle = GetComponent<Transform>();
    }

    private void Update()
    {
        if (Input.touchCount != 2) return;
        DoubleTouchRotate();
    }

    private void DoubleTouchRotate()
    {
        _desiredRotation = cameraHandle.transform.rotation;

        DetectTouchMovement.Calculate();

        Vector3 rotationDeg = Vector3.zero;
        rotationDeg.y = DetectTouchMovement.turnAngleDelta;
        _desiredRotation *= Quaternion.Euler(rotationDeg);

        cameraHandle.rotation = _desiredRotation;
    }

    private void DoubleTouchZoom()
    {
        Touch firstTouch = Input.GetTouch(0);
        Touch secondTouch = Input.GetTouch(1);

        firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
        secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

        touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
        touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

        zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;

        if (touchesPrevPosDifference > touchesCurPosDifference + 20.0f)
        {
            float Ypos = mainCamera.transform.localPosition.y;
            float Zpos = mainCamera.transform.localPosition.z;

            Ypos += zoomModifier;
            Zpos -= zoomModifier;

            mainCamera.transform.localPosition = new Vector3(mainCamera.transform.localPosition.x,
                Mathf.Clamp(Ypos, 2.0f, 20.0f), Mathf.Clamp(Zpos, -20.0f, -2.0f));
        }

        if (touchesPrevPosDifference < touchesCurPosDifference - 20.0f)
        {
            float Ypos = mainCamera.transform.localPosition.y;
            float Zpos = mainCamera.transform.localPosition.z;

            Ypos -= zoomModifier;
            Zpos += zoomModifier;

            mainCamera.transform.localPosition = new Vector3(mainCamera.transform.localPosition.x,
                Mathf.Clamp(Ypos, 2.0f, 20.0f), Mathf.Clamp(Zpos, -20.0f, -2.0f));
        }
    }
}