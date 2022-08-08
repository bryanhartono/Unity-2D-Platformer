using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed;
    public int currentPoint;
    public bool goForward;

    public Transform[] points; // remember to add in points manually in unity
    public Transform platform;

    void Start()
    {   

    }

    void Update()
    {
        platform.position = Vector3.MoveTowards(platform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(platform.position, points[currentPoint].position) < 0.05f)
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

    public void CheckDirection()
    {
        if(currentPoint <= 0)
        {
            goForward = true;
        }
            
        if(currentPoint >= points.Length - 1)
        {
            goForward = false;            
        }
    }
}
