using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music_Continue : MonoBehaviour
{
    private static Music_Continue instance;
    public AudioSource BackgroundMusic;
    public AudioClip trackBG;

    void Start()
    {
        if (SceneManager.GetActiveScene().name.Contains("Race"))
        {
            Destroy(gameObject);
            //BackgroundMusic.Pause();
        }
        else
        {
            DontDestroyOnLoad(instance);
            //BackgroundMusic.Play();
        }
    }

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
}
