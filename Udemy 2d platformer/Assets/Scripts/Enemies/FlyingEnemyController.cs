using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    public float moveSpeed, chaseSpeed;
    public float distanceToAttackPlayer, waitAfterAttack;
    public int currentPoint;
    public bool goForward;

    public Transform[] points; // remember to add in points manually in unity
    public SpriteRenderer SR;

    private float attackCounter;

    private Vector3 attackTarget;

    void Start()
    {   
        // for the points
        for(int i = 0; i < points.Length; i++)
        {
            points[i].parent = null;
        }
    }

    void Update()
    {
        if(attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;
        }
        else
        {
            if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) > distanceToAttackPlayer)
            {
                // reset attack target if player moves away
                attackTarget = Vector3.zero;

                transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

                if(Vector3.Distance(transform.position, points[currentPoint].position) < 0.05f)
                {
                    CheckDirection();

                    if(goForward)
                    {
                        currentPoint++;
                    }
                        
                    if(!goForward)
                    {
                        currentPoint--;
                    }
                }
            }
            else
            {
                // Attacking player
                if(attackTarget == Vector3.zero)
                {
                    attackTarget = PlayerController.instance.transform.position;
                }

                AttackDirection();
                transform.position = Vector3.MoveTowards(transform.position, attackTarget, chaseSpeed * Time.deltaTime);

                if(Vector3.Distance(transform.position, attackTarget) <= 0.1f)
                {
                    attackCounter = waitAfterAttack;
                    attackTarget = Vector3.zero;
                }
            }
        }
    }

    public void CheckDirection()
    {
        if(currentPoint <= 0)
        {
            goForward = true;
            SR.flipX = true;
        }
            
        if(currentPoint >= points.Length - 1)
        {
            goForward = false;    
            SR.flipX = false;        
        }
    }

    public void AttackDirection()
    {
        if(transform.position.x < PlayerController.instance.transform.position.x)
        {
            SR.flipX = true;
        }
                
        if(transform.position.x > PlayerController.instance.transform.position.x)
        {
            SR.flipX = false;
        }
    }
}
