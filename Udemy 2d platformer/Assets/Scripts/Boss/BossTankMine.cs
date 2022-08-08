using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankMine : MonoBehaviour
{
    public GameObject explosion;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);

            PlayerHealthController.instance.DealDamage();

            AudioManager.instance.PlaySFX(AudioManager.enemyExplode);
        }
    }

    public void Explode()
    {
        Destroy(gameObject);
        Instantiate(explosion, transform.position, transform.rotation);
        AudioManager.instance.PlaySFX(AudioManager.enemyExplode);
    }
}
