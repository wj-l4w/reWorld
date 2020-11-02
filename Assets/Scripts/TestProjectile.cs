using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    public float damage;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name !="Player")
        {
            if (collision.GetComponent<ReceiveDmg>() != null)
            {
                collision.GetComponent<ReceiveDmg>().DealDmg(damage);
            }
            Destroy(gameObject);
        }
    }
}
