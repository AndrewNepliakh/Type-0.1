using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlasma : MonoBehaviour {

    private Transform target;
    public int damageValue;
    public float speed = 70f;
    public GameObject hitSparks;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {

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
        GameObject hitSparksIns = (GameObject)Instantiate(hitSparks, transform.position, transform.rotation);
        Destroy(hitSparksIns, 1f);
        Destroy(gameObject);
        Damage(target);
    }

    void Damage(Transform enemy)
    {
        Enemy target = enemy.GetComponent<Enemy>();
        EnemyResp respTarg = enemy.GetComponent<EnemyResp>();
        if (target != null) target.TakeDamage(damageValue);    
        if (respTarg != null) respTarg.TakeDamage(damageValue);
    }
}
