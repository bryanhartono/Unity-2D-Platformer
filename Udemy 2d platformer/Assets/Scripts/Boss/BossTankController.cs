using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour
{
    public enum bossStates {
        shooting,
        hurt,
        moving,
        ended
    };
    public bossStates currentState;

    public Transform theBoss;
    public Animator anim;
    
    [Header("Movement")]
    public float moveSpeed;
    private bool moveRight;
    public float timeBetweenMines;
    private float mineCounter;
    public Transform leftPoint, rightPoint;
    public GameObject mine;
    public Transform minePoint;

    [Header("Shooting")]
    public float timeBetweenShots;
    private float shotCounter;
    public Transform firePoint; // where the bullets are shot from
    public GameObject bullet;

    [Header("Hurt")]
    public float hurtTime;
    private float hurtCounter;
    public GameObject hitBox;

    [Header("Health")]
    public int health;
    public float shotSpeedUp, mineSpeedUp;
    public GameObject explosion, winPlatform;
    private bool isDefeated;

    void Start()
    {
        currentState = bossStates.shooting; // or we can type in "currentState = 0;"
    }

    void Update()
    {
        switch(currentState)
        {
            case bossStates.shooting:

                shotCounter -= Time.deltaTime;

                if(shotCounter <= 0)
                {
                    shotCounter = timeBetweenShots;

                    // instantiate new bullet to give it the boss tank's scale
                    var newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                    newBullet.transform.localScale = theBoss.localScale;
                }

                break;

            case bossStates.hurt:

                if(hurtCounter > 0)
                {
                    hurtCounter -= Time.deltaTime;

                    if(hurtCounter <= 0)
                    {
                        currentState = bossStates.moving;

                        mineCounter = 0;

                        if(isDefeated)
                        {
                            theBoss.gameObject.SetActive(false);
                            Instantiate(explosion, theBoss.position, theBoss.rotation);

                            winPlatform.SetActive(true);
                            AudioManager.instance.StopBossMusic();

                            currentState = bossStates.ended;
                        }
                    }
                }

                break;

            case bossStates.moving:

                if(moveRight)
                {
                    theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if(theBoss.position.x > rightPoint.position.x)
                    {
                        theBoss.localScale = Vector3.one;
                        moveRight = false;
                        EndMovement();
                    }
                }
                else
                {
                    theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if(theBoss.position.x < leftPoint.position.x)
                    {
                        theBoss.localScale = new Vector3(-1f, 1f, 1f);
                        moveRight = true;
                        EndMovement();
                    }
                }

                mineCounter -= Time.deltaTime;

                if(mineCounter <= 0)
                {
                    mineCounter = timeBetweenMines;
                    Instantiate(mine, minePoint.position, minePoint.rotation);
                }

                break;
        }
        
        // the code below only runs on unity editor
        #if UNITY_EDITOR

        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeHit();
        }

        #endif
    }

    public void TakeHit()
    {
        currentState = bossStates.hurt;
        hurtCounter = hurtTime;

        anim.SetTrigger("Hit");

        AudioManager.instance.PlaySFX(AudioManager.bossHit);

        health--;

        if(health <= 0)
        {
            BossTankMine[] mines = FindObjectsOfType<BossTankMine>();
            if(mines.Length > 0)
            {
                foreach(BossTankMine mine in mines)
                {
                    mine.Explode();
                }
            }

            isDefeated = true;
        }
        else
        {
            timeBetweenShots /= shotSpeedUp;
            timeBetweenMines /= mineSpeedUp;
        }
    }

    private void EndMovement()
    {
        currentState = bossStates.shooting;
        shotCounter = 0f;
        anim.SetTrigger("StopMoving");

        hitBox.SetActive(true);
    }

}
