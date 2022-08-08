using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitToRespawn;
    public float timeInLevel;
    public int gemsCollected;
    public string levelToLoad;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        timeInLevel = 0;
    }

    void Update()
    {
        // updates timer
        timeInLevel += Time.deltaTime;
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }

    private IEnumerator RespawnCo()
    {
        // Make player disappear
        PlayerController.instance.gameObject.SetActive(false);
        AudioManager.instance.PlaySFX(AudioManager.playerDeath);

        yield return new WaitForSeconds(waitToRespawn - (1.0f / UIController.instance.fadeSpeed));

        // fades screen to black on player's death
        UIController.instance.FadeToBlack();

        // wait for a short while, then shows the screen again
        yield return new WaitForSeconds((1.0f / UIController.instance.fadeSpeed) + 0.2f);
        UIController.instance.FadeFromBlack();

        // Respawn player
        PlayerController.instance.gameObject.SetActive(true);

        // Teleport player back to startingPoint or last checkpoint and resets the player's health
        PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        UIController.instance.UpdateHealthDisplay();
    }

    public IEnumerator EndLevelCo()
    {
        // plays level end music
        AudioManager.instance.PlayLevelVictory();

        PlayerController.instance.stopInput = true;
        CameraController.instance.stopFollow = true;
        UIController.instance.levelCompleteText.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1.0f / UIController.instance.fadeSpeed) + 3.0f);

        // saves unlocked level, ex. level1-1_unlocked = 1
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);

        // always save highest gems
        if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_gems"))
        {
            if(gemsCollected > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_gems"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);
            }
        }
        else
        {
            // saves number of gems collected after finishing a level
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);
        }

        // always save lowest time
        if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_time"))
        {
            if(timeInLevel < PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_time"))
            {
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
            }
        }
        else
        {
            // saves amount of time spent after finishing a level
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
        }
        
        // loads next scene
        SceneManager.LoadScene(levelToLoad);
    }
}
