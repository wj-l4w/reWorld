using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpell : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float lifeTime;

    public float damage;

    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame

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
    private void Update()
    {
        transform.Translate(speed * Time.deltaTime, speed * Time.deltaTime, 0);
    }

    void DestroyProjectile()
    {
        //Instantiate(transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
