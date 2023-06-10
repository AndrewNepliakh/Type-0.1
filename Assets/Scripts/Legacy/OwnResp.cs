using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OwnResp : MonoBehaviour {

    public int healthPoint = 50;
    private float currentHealth;
    public Image healthBar;
    //public Text healthPointText;
    public GameObject explosionFX;
    public GameObject brokenOwnResp;

    void Start()
    {
        currentHealth = healthPoint;
    }

    void Update()
    {
        healthBar.fillAmount = healthPoint / currentHealth;
        //healthPointText.text = healthPoint.ToString();
        if ( healthPoint <= 0)
        {
            Termination();
        }
    }

    public void TakingDamage(int damage)
    {
        healthPoint -= damage;
    }

    void Termination()
    {
        Instantiate(explosionFX, transform.position, transform.rotation);
        Instantiate(brokenOwnResp, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
