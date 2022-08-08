using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public static bool hasWon;

    void Start()
    {
        hasWon = false;
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            hasWon = true;
            LevelManager.instance.EndLevel();
        }
    }
}
