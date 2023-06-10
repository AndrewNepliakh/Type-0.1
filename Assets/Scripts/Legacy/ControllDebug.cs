using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllDebug : MonoBehaviour {

    Camera cam;

	void Start () {
        cam = Camera.main;
	}
	

	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("We hit " + hit.collider.name + " " + hit.point);
            }
        }
	}
}
