using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Mage : NetworkBehaviour
{
    [Header("Stats")]
    public int damage;
    private float timeBtwAtk = 0;
    public float startTimeBtwAtk;
    public float atkRange;
    public Transform atkPos;
    
    [Header("Other Stuffs")]
    public LayerMask WhatIsEnemies;
    public Animator animator;
    public float offset;
    public GameObject weapon;
    public Player player;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void warriorUpdate()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        weapon.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        if (rotZ > -90 && rotZ <= 90)
        {
            animator.SetFloat("MouseHorizontal", 1f);
        }
        else{
            animator.SetFloat("MouseHorizontal", 0f);
        }


        if (timeBtwAtk <= 0)
        {
            //Cooldown is complete, can atk
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("Player attacked");
                animator.SetTrigger("isAttacking");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(atkPos.position, atkRange, WhatIsEnemies);
                foreach(Collider2D enemy in enemiesToDamage)
                {
                    
                    player.CmdDealDamage(enemy.GetComponent<EnemyController>().netId, damage);
                }

                timeBtwAtk = startTimeBtwAtk;
            }
        }
        else
        {
            timeBtwAtk -= Time.deltaTime;
        }
            
       
        




/*        if (timeBtwAtk <= 0)
        {
            //Cooldown is complete, can atk
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("Player attacked");
                animator.SetBool("IsAttacking", true);
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(atkPos.position, atkRange, WhatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyController>().cmdTakeDamage(damage);
                }
            }
            timeBtwAtk = startTimeBtwAtk;
        }
        else
        {
            animator.SetBool("IsAttacking", false);
            timeBtwAtk -= Time.deltaTime;
        }*/
    }

    private void OnDrawGizmosSelected()
    {
        if (atkPos == null) { return; }

        Gizmos.DrawWireSphere(atkPos.position, atkRange);
    }
}
