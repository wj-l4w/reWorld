using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveDmg : MonoBehaviour
{
    public float health;
    public float maxHealth;

    // Start is called before the first frame update
    private void Start()
    {
        health = maxHealth;
    }

    public void DealDmg(float damage)
    {
        health -= damage;
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void CheckOverHeal()
    {
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }
}
