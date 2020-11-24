using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Mage : NetworkBehaviour
{
    [Header("Stats")]
    private float timeBtwAtk = 0;
    public int fireballLevel;
    public float startTimeBtwAtk;
    public Transform atkPos;
    
    [Header("Other Stuffs")]
    public LayerMask WhatIsEnemies;
    public Animator animator;
    [SyncVar]
    public float offset;
    public GameObject weapon;
    public Fireball fireSpell;
    public Player player;
    [SyncVar]
    public float rotZ;
    private Quaternion rotation;

    private void Start()
    {
        animator = GetComponent<Animator>();
        fireballLevel = 1;
    }

    public void mageUpdate()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        rotation = weapon.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
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
                
                FindObjectOfType<AudioManager>().Play("Fireball");

                CmdSpawnFireball(rotZ);

                timeBtwAtk = startTimeBtwAtk;
            }
        }
        else
        {
            timeBtwAtk -= Time.deltaTime;
        }

    }

    [Command]
    public void CmdSpawnFireball(float rot)
    {
        Fireball fireball = Instantiate(fireSpell, atkPos.position, Quaternion.Euler(0f, 0f, rot + offset));
        fireball.playerId = player.netId;
        fireball.damage *= player.mageScript.fireballLevel;
        fireball.speed = 8;
        fireball.lifeTime = 1.5f;
        NetworkServer.Spawn(fireball.gameObject);
    } 

}
