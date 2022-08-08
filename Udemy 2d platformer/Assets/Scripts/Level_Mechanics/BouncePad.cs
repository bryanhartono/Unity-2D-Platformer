using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bounceForce = 20f;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController.instance.rigidbody2D.velocity = new Vector2(PlayerController.instance.rigidbody2D.velocity.x, bounceForce);
            anim.SetTrigger("Bounce");
        }
    }
}
