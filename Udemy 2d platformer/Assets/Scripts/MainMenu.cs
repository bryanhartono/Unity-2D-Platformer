using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string startScene, continueScene;

    public GameObject continueButton;

    void Start()
    {
        if(PlayerPrefs.HasKey(startScene + "_unlocked"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startScene);

        // Starts a new game save file
        PlayerPrefs.DeleteAll();
    }

    public void QuitGame()
    {
        Application.Quit();
        // Debug.Log("Quitting game");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(continueScene);
    }
}
