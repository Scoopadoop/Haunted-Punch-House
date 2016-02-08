using UnityEngine;
using System.Collections;

public class EnemyHealthManager : MonoBehaviour
{
    public int enemyHealth;


    public GameObject deathEffect;

    public int pointsOnDeath;
	// Use this for initialization
	void Start () {
        enemyHealth = 3;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (enemyHealth <= 0)
        {
            //gameObject.GetComponent<Animation>().Play("death animation");
            //insert the death animation in the above line
            Destroy(gameObject);
        }

    }

    public void Damage(int damage)
    {
        enemyHealth -= damage;
    }
}
