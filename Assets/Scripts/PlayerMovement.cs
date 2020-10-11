using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Vector2 direction;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();    
    }
    private void Update()
    {
        GetInput();
        Move();
    }

    private void GetInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W)){
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A)){
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S)){
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D)){
            direction += Vector2.right;
        }
    }

    private void Move()
    {
        transform.Translate(speed * direction * Time.deltaTime);
        SetAnimatorMovement(direction);
    }

    private void SetAnimatorMovement(Vector2 direction)
    {
        animator.SetFloat("xDirection", direction.x);
        animator.SetFloat("yDirection", direction.y);
        print(animator.GetFloat("xDirection"));
        print(animator.GetFloat("yDirection"));
    }
}
