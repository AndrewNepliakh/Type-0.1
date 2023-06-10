using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampStage : MonoBehaviour
{
    public float range;
    public GameObject cam;

    void Update()
    {
        if (Vector3.Distance(transform.position, cam.transform.position) <= range)
        {
      
            Color color = gameObject.GetComponent<Renderer>().material.color;
            color.a = 0.0f;

            gameObject.GetComponent<Renderer>().material.color = color;
        }

        if (Vector3.Distance(transform.position, cam.transform.position) > range)
        {
            Color color = gameObject.GetComponent<Renderer>().material.color;
            color.a += 0.01f;

            if (color.a == 1.0f) color.a = 1.0f;

            gameObject.GetComponent<Renderer>().material.color = color;
        }
    }
}
