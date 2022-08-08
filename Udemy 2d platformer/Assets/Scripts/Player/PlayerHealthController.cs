using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth;
    public int maxHealth;
    public float invincibleLength;

    public GameObject deathEffect;

    private float invincibleCounter;

    private SpriteRenderer SR;
    
    void Awake()
    {
        // allows us to access this script from anywhere else
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        SR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;
            
            if(invincibleCounter <= 0)
            {
                SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, 1.0f);
            }
            }    
    }

    public void DealDamage()
    {
        if(invincibleCounter <= 0)
        {
            // player hits an enemy, minus hp by 1
            currentHealth--;

            if(currentHealth <= 0)
            {
                // to avoid any weird errors
                currentHealth = 0;

                Instantiate(deathEffect, transform.position, transform.rotation);

                LevelManager.instance.RespawnPlayer();
            } 
            else
            {
                invincibleCounter = invincibleLength;
                SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, 0.5f);

                AudioManager.instance.PlaySFX(AudioManager.playerHurt);

                PlayerController.instance.knockBack();
            }

            UIController.instance.UpdateHealthDisplay();
        }
    }

    public void HealPlayer()
    {
        currentHealth++;

        // avoid weird bugs
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealthDisplay();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Platform"))
        {
            transform.parent = other.transform;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Platform"))
        {
            transform.parent = null;
        }
    }
}
