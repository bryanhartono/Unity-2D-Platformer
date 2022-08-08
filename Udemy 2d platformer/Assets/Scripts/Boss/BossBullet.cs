using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed;

    void Start()
    {
        AudioManager.instance.PlaySFX(AudioManager.bossShot);
    }

    void Update()
    {
        transform.position += new Vector3(-speed * transform.localScale.x * Time.deltaTime, 0f, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerHealthController.instance.DealDamage();
        }

        AudioManager.instance.PlaySFX(AudioManager.bossImpact);
        
        Destroy(gameObject);
    }

}
