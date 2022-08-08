using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stompbox : MonoBehaviour
{
    [Range(0, 100)] public float chanceToDrop;

    public GameObject deathEffect;
    public GameObject collectible;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            // destroy enemy
            other.transform.parent.gameObject.SetActive(false);
            Instantiate(deathEffect, other.transform.position, other.transform.rotation);

            // player bounces after landing on enemy
            PlayerController.instance.Bounce();

            // enemies has a 40 percent chance of dropping a health pickup
            float dropSelect = Random.Range(0.0f, 100.0f);
            if(dropSelect <= chanceToDrop)
            {
                Instantiate(collectible, other.transform.position, other.transform.rotation);
            }

            // sound effect of enemy death
            AudioManager.instance.PlaySFX(AudioManager.enemyExplode);
        }
    }
}
