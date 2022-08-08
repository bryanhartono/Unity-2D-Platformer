using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    // ------------------------------------
    // sfx index -----------------------
    public static int bossHit = 0;
    public static int bossImpact = 1;
    public static int bossShot = 2;
    public static int enemyExplode = 3;
    public static int levelSelected = 4;
    public static int mapMovement = 5;
    public static int pickupGem = 6;
    public static int pickupHealth = 7;
    public static int playerDeath = 8;
    public static int playerHurt = 9;
    public static int playerJump = 10;
    public static int warpJingle = 11;
    // ------------------------------------

    public AudioSource bgm, levelEndMusic, bossMusic;
    public AudioSource[] soundEffects;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlaySFX(int soundToPlay)
    {
        // avoids to sfx playing at the same time
        soundEffects[soundToPlay].Stop();

        soundEffects[soundToPlay].pitch = Random.Range(0.9f, 1.2f);

        // plays sfx
        soundEffects[soundToPlay].Play();
    }

    public void PlayLevelVictory()
    {
        bgm.Stop();
        levelEndMusic.Play();
    }

    public void PlayBossMusic()
    {
        bgm.Stop();
        bossMusic.Play();
    }

    public void StopBossMusic()
    {
        bossMusic.Stop();
        bgm.Play();
    }
}
