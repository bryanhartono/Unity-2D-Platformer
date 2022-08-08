using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool deactivateOnSwitch;

    public GameObject objectToSwitch;
    public Sprite downSprite, upSprite;

    private SpriteRenderer SR;

    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
           
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // activate and deactivate object
            if(deactivateOnSwitch)
            {
                objectToSwitch.SetActive(false);
            }
            else
            {
                objectToSwitch.SetActive(true);
            }  

            // change the sprite
            if(SR.sprite == upSprite)
            {
                SR.sprite = downSprite;
            }
            else
            {
                SR.sprite = upSprite;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(deactivateOnSwitch)
            {
                deactivateOnSwitch = false;
            }
            else
            {
                deactivateOnSwitch = true;
            }
        }
    }
}
