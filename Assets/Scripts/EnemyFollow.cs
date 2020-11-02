using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    private Transform Target;

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, Target.position) <= 3 ){
            if(Vector2.Distance(transform.position, Target.position) > stoppingDistance){
            transform.position = Vector2.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
            }
        }
        
        
    }
}
