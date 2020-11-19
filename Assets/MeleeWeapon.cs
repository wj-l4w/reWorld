using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public float offset;
    public Transform shotPoint;
    public float attackRange;
    public LayerMask WhatIsEnemies;

    private float AttackTime;
    public float AttackStartTime;

    public int damage;

    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if(AttackTime <= 0)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(shotPoint.position, attackRange, WhatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<ReceiveDmg>().DealDmg(damage);
                }
                AttackTime = AttackStartTime;
            }
        } else {
            AttackTime -= Time.deltaTime;
        }

        
    }

    void OnDrawGizmosSelected(){
        Gizmos.color =Color.red;
        Gizmos.DrawWireSphere(shotPoint.position, attackRange);
    }
}
