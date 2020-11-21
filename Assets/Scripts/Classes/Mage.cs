using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Mage : NetworkBehaviour
{
    [Header("Stats")]
    private float timeBtwAtk = 0;
    public float startTimeBtwAtk;
    public Transform atkPos;
    
    [Header("Other Stuffs")]
    public LayerMask WhatIsEnemies;
    public Animator animator;
    public float offset;
    public GameObject weapon;
    public Fireball fireSpell;
    public Player player;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void mageUpdate()
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
                Debug.Log("Player " + player.netId + " (mage) casted a fireball!");
                animator.SetTrigger("mageIsAttacking");
                //Cast fireball
                Fireball fireball = Instantiate(fireSpell, atkPos.position, weapon.transform.rotation);
                fireball.player = player;
                NetworkServer.Spawn(fireball.gameObject);

                timeBtwAtk = startTimeBtwAtk;
            }
        }
        else
        {
            timeBtwAtk -= Time.deltaTime;
        }
            
    }
}
