using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour {

    public GameObject brokenEnemy;

    void OnMouseDown()
    {
        Instantiate(brokenEnemy, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
