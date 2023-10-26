using Player;
using Services.Factory;
using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    [Inject] private IGameObjectsFactory _gameObjectsFactory;
    
    public Vector3 offset;
    public float currentZoom = 10f;
    public float pitch = 2f;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;
    public float yawSpeed = 150f;
    public float panBorderThickness = 10f;
    
    private float currentYaw = 0f;
    private Transform target;

    private void Start()
    {
        target = _gameObjectsFactory.GetGameObject<PlayerController>().transform;
    }

    // void Update()
    // {
    //     currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
    //     currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    //     
    //     if (Input.GetMouseButton(1))
    //     {
    //         currentYaw += Input.GetAxis("Mouse X") * yawSpeed * Time.deltaTime;
    //     }
    // }

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position;
            // transform.LookAt(target.position + Vector3.up * pitch);
            // transform.RotateAround(target.position, Vector2.up, currentYaw);
        }
    }
}