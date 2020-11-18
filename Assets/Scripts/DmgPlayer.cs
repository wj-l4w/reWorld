﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class DmgPlayer : NetworkBehaviour
{
    private HealthManager healthMan;
    public Player target;
    private float DmgCd = 0f;
    private bool isTouching;
    private bool isAttacking;
    [SerializeField] private int DmgToGive = 10;
    // Start is called before the first frame update
    void Start()
    {
        healthMan = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(reloading){
            loadingTime -= Time.deltaTime;
            if(loadingTime<=0){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }*/

        // if(isTouching){
        //     DmgCd -= Time.deltaTime;
        //     if(DmgCd <= 0){
        //         healthMan.DmgPlayer(DmgToGive);
        //         DmgCd = 2f;
        //     }
        // }

        attack();

    }

    
    private void attack()
    {
        if (isAttacking)
        {
            DmgCd -= Time.deltaTime;
            if (DmgCd <= 0)
            {
                RpcDamage(target.connectionToClient);
                DmgCd = 2f;
            }
        }
    }

    [TargetRpc]
    public void RpcDamage(NetworkConnection conn)
    {
        target.currentHealth -= DmgToGive;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag == "Player"){
            //other.gameObject.GetComponent<HealthManager>().DmgPlayer(DmgToGive);
            isAttacking = true;
            // reloading = true;
        }  
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            //other.gameObject.GetComponent<HealthManager>().DmgPlayer(DmgToGive);
            isAttacking = false;
            // reloading = true;
        }
    }

    // private void OnCollisionStay2D(Collision2D other) {
    //     isTouching = true;

    // }
}
