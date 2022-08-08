using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public SpriteRenderer SR;
    public Sprite cp0n, cpOff;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // better than other.tag == "Player"
        if(other.CompareTag("Player"))
        {
            // resets all checkpoints
            CheckpointController.instance.DeactivateCheckpoints();

            SR.sprite = cp0n;

            CheckpointController.instance.SetSpawnPoint(transform.position);
        }
    }

    public void ResetCheckpoint()
    {
        SR.sprite = cpOff;
    }
}
