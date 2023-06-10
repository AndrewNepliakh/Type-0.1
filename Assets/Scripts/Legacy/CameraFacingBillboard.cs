using UnityEngine;
using System.Collections;

public class CameraFacingBillboard : MonoBehaviour
{
    public string cameraTag = "MainCamera";

    void Update()
    {
        GameObject m_Camera = GameObject.FindGameObjectWithTag(cameraTag);
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);
    }
}