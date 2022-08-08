using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LSUIController : MonoBehaviour
{
    public static LSUIController instance;

    public float fadeSpeed;
    
    public Image fadeScreen;
    public GameObject levelInfoPanel;
    public Text levelName;
    public Text gemsFound, gemsTarget;
    public Text bestTime, timeTarget;

    private bool shouldFadeFromBlack, shouldFadeToBlack;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        FadeFromBlack();
    }

    void Update()
    {
        if(shouldFadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1.0f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 1.0f)
            {
                shouldFadeToBlack = false;
            }
        }

        if(shouldFadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0.0f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 0.0f)
            {
                shouldFadeFromBlack = false;
            }
        }
    }

    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        shouldFadeToBlack = false;
        shouldFadeFromBlack = true;
    }

    public void ShowInfo(MapPoint levelInfo)
    {
        // update level info
        levelName.text = levelInfo.levelName;

        gemsFound.text = "FOUND: " + levelInfo.gemsCollected;
        gemsTarget.text = "IN LEVEL: " + levelInfo.totalGems;

        if(levelInfo.bestTime == 0)
        {
            bestTime.text = "BEST: ---";
        }
        else
        {
            // F1 -> float number with 1 decimal place
            bestTime.text = "BEST: " + levelInfo.bestTime.ToString("F1") + "s";
        }
        timeTarget.text = "TARGET: " + levelInfo.targetTime.ToString("F1") + "s";

        levelInfoPanel.SetActive(true);
    }

    public void HideInfo()
    {   
        levelInfoPanel.SetActive(false);
    }
}
