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
    [SerializeField] private int mobId;
    [SerializeField] private int exp;
    [SerializeField] private float expRange = 5.5f;
    public LayerMask playersMask;


    void Start()
    {
        myAnim = GetComponent<Animator>();
        currentHealth = maxHealth;
        //enemyAttack = GetComponent<EnemyAttack>();
    }

    void FixedUpdate()
    {
        if (FindClosestPlayer() == null)
        {
            goHome();
            myAnim.SetBool("isAttacking", false);
        }
        else
        {
            targetPlayer = FindClosestPlayer().GetComponent<Player>();
            target = targetPlayer.transform;
            //enemyAttack.target = targetPlayer.GetComponent<Player>();

            if (Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange)
            {
                followPlayer();
                myAnim.SetBool("isAttacking", false);
            }
            else if (Vector3.Distance(target.position, transform.position) < 2)
            {
                isAttacking = true;

                if (isAttacking)
                {
                    //Debug.Log(netId + " is aggroed on player " + targetPlayer.netId);
                    attackCd -= Time.deltaTime;
                    myAnim.SetBool("isAttacking", false);
                    myAnim.SetBool("isMoving", false);
                    if (attackCd <= 0)
                    {
                        if (mobId == 0)
                        {
                            FindObjectOfType<AudioManager>().Play("Cat");
                        }
                        else if (mobId == 1)
                        {
                            FindObjectOfType<AudioManager>().Play("Cobra");
                        }
                        else if (mobId == 2)
                        {
                            FindObjectOfType<AudioManager>().Play("Gladiator");
                        }
                        else if (mobId == 3)
                        {
                            FindObjectOfType<AudioManager>().Play("MiniGolem");
                        }
                        else if (mobId == 4)
                        {
                            FindObjectOfType<AudioManager>().Play("Slime");
                        }
                        myAnim.SetBool("isAttacking", true);
                        attackAnimCd -= Time.deltaTime;
                        if (attackAnimCd <= 0)
                        {
                            attackCd = 1f;
                            attackAnimCd = 0.45f;
                            RpcDmgPlayer(targetPlayer.connectionToClient, damage);
                        }

                    }
                }

            }
            else if (Vector3.Distance(target.position, transform.position) > maxRange)
            {
                goHome();
                myAnim.SetBool("isAttacking", false);
            }
        }


    }

    public void followPlayer()
    {
        myAnim.SetBool("isMoving", true);
        myAnim.SetFloat("moveX", (target.position.x - transform.position.x));
        myAnim.SetFloat("moveY", (target.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    public void goHome()
    {
        myAnim.SetFloat("moveX", (homePos.position.x - transform.position.x));
        myAnim.SetFloat("moveY", (homePos.position.y - transform.position.y));
        if (Vector3.Distance(transform.position, homePos.position) == 0)
        {
            myAnim.SetBool("isMoving", false);
        }
        transform.position = Vector3.MoveTowards(transform.position, homePos.position, speed * Time.deltaTime);
    }

    public GameObject FindClosestPlayer()
    {
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 0)
        {
            return null;
        }
        GameObject closest = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in players)
        {

            if (!go.GetComponent<Player>().isDead)
            {
                Vector3 diff = go.transform.position - position;
                float currentDistance = diff.sqrMagnitude;
                if (currentDistance < shortestDistance)
                {
                    closest = go;
                    shortestDistance = currentDistance;
                }
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
    public void RpcDmgPlayer(NetworkConnection conn, int DmgToGive)
    {
        targetPlayer.takeDamage(DmgToGive);
        
    }

    [ClientRpc]
    public void RpcTakeDamage(int dmgToGive)
    {
        Debug.Log("Enemy " + netId + " has taken " + dmgToGive + " damage.");
        currentHealth -= dmgToGive;
        if (currentHealth <= 0)
        {
            //If die
            //drop loot and exp
            giveExp();

            //Destroy(gameObject);
            NetworkServer.Destroy(gameObject);
        }
    }

    private void giveExp()
    {
        Collider2D[] playerInRange = Physics2D.OverlapCircleAll(transform.position, expRange, playersMask);
        foreach (Collider2D player in playerInRange)
        {
            //Check if its Player
            if (player.GetComponent<Player>() != null)
            {
                Player playa = player.GetComponent<Player>();
                Debug.Log("GiveExp called, giving " + exp + " exp for player " + playa.netId);
                playa.addExp(exp);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, expRange);
    }
}

