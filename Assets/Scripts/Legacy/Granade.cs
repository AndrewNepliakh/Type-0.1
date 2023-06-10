using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour {

    public float delay = 3f;
    float timer;
    bool hasExploded = false;

    public GameObject explodeFx;

	void Start () {
        timer = delay;
	}
	
	void Update ()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
	}

    void Explode()
    {
        Instantiate(explodeFx, transform.position, transform.rotation);

        Collider[] collToDestroy = Physics.OverlapSphere(transform.position, 6f);
        foreach (Collider nearObj in collToDestroy)
        {
            PlayerHealth dest = nearObj.GetComponent<PlayerHealth>();
            if (dest != null) dest.Termination();
        }

        Collider[] collToMove = Physics.OverlapSphere(transform.position, 6f);
        foreach (Collider nearObj in collToMove)
        {
            Rigidbody rb = nearObj.GetComponent<Rigidbody>();
            if (rb != null) rb.AddExplosionForce(50f, transform.position, 6f); 
        }

        Destroy(gameObject);
    }
}
