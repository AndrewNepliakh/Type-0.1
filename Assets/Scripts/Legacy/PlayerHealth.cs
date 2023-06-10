using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int healthPoint = 50;
    public int currentHealth;
    //public Text healthPointText;
    public GameObject deadSheep;
    public Vector3 offset;

	void Start () {
        currentHealth = healthPoint;
	}
	
	void Update () {

       // healthPointText.text = currentHealth.ToString();

        if (currentHealth <= 0)
        {
            Termination();
        }
    }

    public void TakingDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void Termination()
    {
        Instantiate(deadSheep, transform.position, transform.rotation);
        Destroy(gameObject);
    } 
}
