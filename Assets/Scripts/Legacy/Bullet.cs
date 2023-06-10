using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;
    public int damageValue;
    public float speed = 70f;
    public GameObject hitSparks;

    public void Seek(Transform _target)
    {
        target = _target;
    }

	void Update () {

        if (target == null)
        {
            Destroy(gameObject);
                return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
	}

    void HitTarget()
    {
        GameObject hitSparksIns = (GameObject) Instantiate(hitSparks, transform.position, Quaternion.identity);
        Destroy(hitSparksIns, 1f);
        Destroy(gameObject);
        Damage(target);
    }

    void Damage(Transform enemy)
    {
        Enemy target = enemy.GetComponent<Enemy>();
        if (target != null)
        {
            target.TakeDamage(damageValue);
        }
    }
}
