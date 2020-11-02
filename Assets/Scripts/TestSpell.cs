using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class TestSpell : MonoBehaviour
{
    public GameObject projectile;
    public float minDmg;
    public float maxDmg;
    public float projectileForce;
    private Transform aimTransform;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 myPos = transform.position;
            Vector2 direction = (mousePos - myPos).normalized;
            
            spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            spell.GetComponent<TestProjectile>().damage = Random.Range(minDmg, maxDmg);
        }
    }

}
