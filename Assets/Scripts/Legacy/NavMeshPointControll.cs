using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshPointControll : MonoBehaviour {

    public float speed;
    public float speedRotate;
    public GameObject player;

    void Update ()
    {
        if (Input.GetKey(KeyCode.U)) transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (Input.GetKey(KeyCode.J)) transform.Translate(Vector3.back * Time.deltaTime * speed);
        if (Input.GetKey(KeyCode.H)) transform.Translate(Vector3.left * Time.deltaTime * speed);
        if (Input.GetKey(KeyCode.K)) transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
