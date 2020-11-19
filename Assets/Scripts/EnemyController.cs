using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator myAnim;
    private Transform target;
    public Transform homePos;
    private float attackCd = 0f;
    private float attackAnimCd = 0.5f;
    private bool isAttacking;
    [SerializeField] private float speed = 0f;
    [SerializeField] private float minRange = 0f;
    [SerializeField] private float maxRange = 0f;


    void Start()
    {
        myAnim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
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
}
