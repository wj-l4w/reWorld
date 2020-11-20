using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EnemyController : NetworkBehaviour
{
    private Animator myAnim;
    private Transform target;
    [SyncVar]
    public Transform homePos;
    [Header("Stats")]
    private float attackCd = 1f;
    private float attackAnimCd = 0.5f;
    private bool isAttacking;
    private Player targetPlayer;
    [SyncVar]
    public int currentHealth;
    [SyncVar]
    public int maxHealth;
    [SerializeField] private int damage;
    [SerializeField] private float speed = 0f;
    [SerializeField] private float minRange = 0f;
    [SerializeField] private float maxRange = 0f;


    void Start()
    {
        myAnim = GetComponent<Animator>();
        currentHealth = maxHealth;
        //enemyAttack = GetComponent<EnemyAttack>();
    }

    void Update()
    {
        targetPlayer = FindClosestPlayer().GetComponent<Player>();
        target = targetPlayer.transform;
        //enemyAttack.target = targetPlayer.GetComponent<Player>();

        if(Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange){
            followPlayer();
            myAnim.SetBool("isAttacking",false);
        }
        else if(Vector3.Distance(target.position, transform.position) < 1){
            isAttacking = true;

            if(isAttacking){
                attackCd -= Time.deltaTime;
                myAnim.SetBool("isAttacking", false);
                myAnim.SetBool("isMoving", false);
                if(attackCd <= 0){
                    myAnim.SetBool("isAttacking",true);
                    attackAnimCd -= Time.deltaTime;
                    if(attackAnimCd <= 0){
                        attackCd = 1f;
                        attackAnimCd = 0.45f;
                        rpcDmgPlayer(targetPlayer.connectionToClient, damage);
                    }
                    
                }
            }
            
        }
        else if(Vector3.Distance(target.position, transform.position) > maxRange) {
            goHome();
            myAnim.SetBool("isAttacking",false);
        }

    }

    public void followPlayer(){
        myAnim.SetBool("isMoving", true);
        myAnim.SetFloat("moveX", (target.position.x - transform.position.x));
        myAnim.SetFloat("moveY", (target.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    public void goHome(){
        myAnim.SetFloat("moveX", (homePos.position.x - transform.position.x));
        myAnim.SetFloat("moveY", (homePos.position.y - transform.position.y));
        if(Vector3.Distance(transform.position, homePos.position) == 0){
            myAnim.SetBool("isMoving", false);
        }
        transform.position = Vector3.MoveTowards(transform.position, homePos.position, speed * Time.deltaTime);
    }

    public GameObject FindClosestPlayer()
    {
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in players)
        {
            Vector3 diff = go.transform.position - position;
            float currentDistance = diff.sqrMagnitude;
            if (currentDistance < shortestDistance)
            {
                closest = go;
                shortestDistance = currentDistance;
            }
        }
        return closest;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        isAttacking = true;
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        isAttacking = false;
    }

    [TargetRpc]
    public void rpcDmgPlayer(NetworkConnection conn, int DmgToGive)
    {
        targetPlayer.takeDamage(DmgToGive);
        Debug.Log(netId + " has hit player " + targetPlayer.netId + " for " + DmgToGive + " damage");
    }

    [ClientRpc]
    public void rpcTakeDamage(int dmgToGive)
    {
        Debug.Log("Enemy " + netId + " has taken " + dmgToGive + " damage.");
        currentHealth -= dmgToGive;
        if (currentHealth <= 0)
        {
            //If die
            //Destroy(gameObject);
            NetworkServer.Destroy(gameObject);

            //drop loot and exp
        }
    }
}

