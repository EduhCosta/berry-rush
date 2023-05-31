using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music_Continue : MonoBehaviour
{
    private static Music_Continue instance;
    public AudioSource BackgroundMusic;
    public AudioClip trackBG;

    private bool _isPlayingMusic = false;

    void Awake()
    {
        if (instance==null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (_isPlayingMusic)
        {
            if (SceneManager.GetActiveScene().name.Contains("Race") || SceneManager.GetActiveScene().name.Contains("Cutscene"))
            {
                BackgroundMusic.Pause();
                _isPlayingMusic = false;
            }
        }
        else
        {
            if (!SceneManager.GetActiveScene().name.Contains("Race") && !SceneManager.GetActiveScene().name.Contains("Cutscene"))
            {
                BackgroundMusic.Play();
                _isPlayingMusic = true;
            }
        }
    }
}
