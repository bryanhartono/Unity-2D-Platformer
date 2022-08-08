using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slammer : MonoBehaviour
{
    public float slamSpeed;
    public float waitAfterSlam;
    public float resetSpeed;

    public Transform theSlammer, slammerTarget, startPosition;

    private float slamCounter;
    private bool slammed;
    private Vector3 slamTarget;

    void Start()
    {
        slammed = false;
    }

    void Update()
    {
        if(theSlammer.position == startPosition.position)
        {
            slammed = false;
        }

        if(slamCounter > 0)
        {
            slamCounter -= Time.deltaTime;
        }
        else
        {
            if(Vector3.Distance(slammerTarget.position, PlayerController.instance.transform.position) > 2.8f || slammed)
            {
                theSlammer.position = Vector3.MoveTowards(theSlammer.position, startPosition.position, resetSpeed * Time.deltaTime);
            }
            else if(Vector3.Distance(slammerTarget.position, PlayerController.instance.transform.position) <= 2.8f && !slammed)
            {   
                theSlammer.position = Vector3.MoveTowards(theSlammer.position, slammerTarget.position, slamSpeed * Time.deltaTime);

                if(Vector3.Distance(theSlammer.position, slammerTarget.position) <= 0.1f)
                {
                    slamCounter = waitAfterSlam;
                    slamTarget = Vector3.zero;
                    slammed = true;
                }
            }
        }
    }
}
