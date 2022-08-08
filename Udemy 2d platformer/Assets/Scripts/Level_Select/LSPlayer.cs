using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSPlayer : MonoBehaviour
{
    public float moveSpeed = 10f;

    public MapPoint currentPoint;
    public LSManager theManager;

    private bool levelLoading;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, currentPoint.transform.position) < 0.1f && !levelLoading)
        {    
            // if player pressed right arrow
            if(Input.GetAxisRaw("Horizontal") > 0.5f)
            {
                if(currentPoint.right != null)
                {
                    SetNextPoint(currentPoint.right);
                }
            }

            // if player pressed left arrow
            if(Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                if(currentPoint.left != null)
                {
                    SetNextPoint(currentPoint.left);
                }
            }

            // if player pressed up arrow
            if(Input.GetAxisRaw("Vertical") > 0.5f)
            {
                if(currentPoint.up != null)
                {
                    SetNextPoint(currentPoint.up);
                }
            }

            // if player pressed down arrow
            if(Input.GetAxisRaw("Vertical") < -0.5f)
            {
                if(currentPoint.down != null)
                {
                    SetNextPoint(currentPoint.down);
                }
            }

            // player presses the space button to enter level
            if(currentPoint.isLevel && currentPoint.levelToLoad != "" && !currentPoint.isLocked)
            {
                LSUIController.instance.ShowInfo(currentPoint);

                if(Input.GetButtonDown("Jump"))
                {
                    levelLoading = true;
                    theManager.LoadLevel();
                }
            }
        }
    }

    public void SetNextPoint(MapPoint nextPoint)
    {
        currentPoint = nextPoint;
        LSUIController.instance.HideInfo();

        AudioManager.instance.PlaySFX(AudioManager.mapMovement);
    }
}
