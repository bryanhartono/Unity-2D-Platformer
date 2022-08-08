using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    public float jumpForce;
    public float knockBackLength;
    public float knockBackForce;
    public float bounceForce;
    public bool stopInput;

    public Rigidbody2D rigidbody2D;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    
    private bool isGrounded;
    private bool canDoubleJump;
    private float knockBackCounter;

    private Animator animator;
    public SpriteRenderer SR;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // if game is in pause mode, disable player keyboard inputs
        if(!PauseMenu.instance.isPaused && !stopInput)
        {
            if(knockBackCounter <= 0)
            {
                // move right or left
                rigidbody2D.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rigidbody2D.velocity.y);

                // check whether player is on the ground or no
                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);

                // enables double jump if player is on the ground
                if (isGrounded)
                {
                    canDoubleJump = true;
                }

                // jump mechanic
                if(Input.GetButtonDown("Jump"))
                {
                    if(isGrounded)
                    {  
                        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
                        AudioManager.instance.PlaySFX(AudioManager.playerJump);
                    }
                    else
                    {
                        if(canDoubleJump)
                        {
                            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
                            canDoubleJump = false;
                            AudioManager.instance.PlaySFX(AudioManager.playerJump);
                        }
                    }
                }

                // flip player to the left;
                if(rigidbody2D.velocity.x < 0)
                {
                    SR.flipX = true;
                }
                else if(rigidbody2D.velocity.x > 0)
                {
                    SR.flipX = false;
                }
            }
            else
            {
                knockBackCounter -= Time.deltaTime;

                if(!SR.flipX)
                {
                    rigidbody2D.velocity = new Vector2(-knockBackForce, rigidbody2D.velocity.y);
                }
                else
                {
                    rigidbody2D.velocity = new Vector2(knockBackForce, rigidbody2D.velocity.y);
                }
            }
        }

        if(!LevelExit.hasWon)
        {
            animator.SetFloat("moveSpeed", Mathf.Abs(rigidbody2D.velocity.x));
            animator.SetBool("isGrounded", isGrounded);
        }
    }

    public void knockBack()
    {
        knockBackCounter = knockBackLength;
        rigidbody2D.velocity = new Vector2(0f, knockBackForce);

        animator.SetTrigger("hurt");
    }

    public void Bounce()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, bounceForce);
        AudioManager.instance.PlaySFX(AudioManager.playerJump);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            animator.SetFloat("moveSpeed", Mathf.Abs(rigidbody2D.velocity.x));
            animator.SetBool("HasWon", LevelExit.hasWon);
        }
    }
}
