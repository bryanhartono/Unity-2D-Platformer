using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    public bool isLevel;
    public bool isLocked;
    public string levelToLoad;
    public string levelToCheck;
    public string levelName;
    public int gemsCollected, totalGems;
    public float bestTime, targetTime;

    public MapPoint up, right, down, left;
    public GameObject gemBadge, timeBadge;

    void Start()
    {
        if(isLevel && levelToLoad != null)
        {
            if(PlayerPrefs.HasKey(levelToLoad + "_gems"))
            {
                gemsCollected = PlayerPrefs.GetInt(levelToLoad + "_gems");
            }

            if(PlayerPrefs.HasKey(levelToLoad + "_time"))
            {
                bestTime = PlayerPrefs.GetFloat(levelToLoad + "_time");
            }

            if(gemsCollected >= totalGems && !isLocked)
            {
                gemBadge.SetActive(true);
            }

            if(bestTime <= targetTime && bestTime != 0 && !isLocked)
            {
                timeBadge.SetActive(true);
            }

            // set all levels as locked first
            isLocked = true;

            if(levelToCheck != null)
            {
                // check if there's value in PlayerPrefs
                if(PlayerPrefs.HasKey(levelToCheck + "_unlocked"))
                {
                    if(PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1)
                    {
                        isLocked = false;
                    }
                }
            }

            if(levelToLoad == levelToCheck)
            {
                isLocked = false;
            }
        }
    }

    
    void Update()
    {
        
    }
}
