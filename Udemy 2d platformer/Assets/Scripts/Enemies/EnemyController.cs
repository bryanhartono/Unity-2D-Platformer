using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public float moveTime, waitTime;
    public bool movingRight;

    public Transform leftPoint, rightPoint;
    public SpriteRenderer SR;

    private float moveCount, waitCount;

    private Rigidbody2D rigidbody2D;
    private Animator animator;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        leftPoint.parent = null;
        rightPoint.parent = null;

        // movingRight = true;

        moveCount = moveTime;
    }

    void Update()
    {
        if(moveCount > 0)
        {
            moveCount -= Time.deltaTime;

            // moves enemies left and right
            if(movingRight)
            {
                rigidbody2D.velocity = new Vector2(moveSpeed, rigidbody2D.velocity.y);
                SR.flipX = true;

                if(transform.position.x > rightPoint.position.x)
                {
                    movingRight = false;
                }
            }
            else
            {
                rigidbody2D.velocity = new Vector2(-moveSpeed, rigidbody2D.velocity.y);
                SR.flipX = false;

                if(transform.position.x < leftPoint.position.x)
                {
                    movingRight = true;
                }
            }

            if(moveCount <= 0)
            {
                waitCount = Random.Range(waitTime * 0.75f, waitTime * 1.25f);
            }

            animator.SetBool("isMoving", true);
        }
        else if(waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            rigidbody2D.velocity = new Vector2(0f, rigidbody2D.velocity.y);

            if(waitCount <= 0)
            {
                moveCount = Random.Range(moveTime * 0.75f, waitTime * 0.75f);;
            }

            animator.SetBool("isMoving", false);
        }
    }
}
