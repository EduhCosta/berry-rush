using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinuousMusic : MonoBehaviour
{
    [SerializeField ]public AudioSource BackgroundMusic;

    void Start()
    {
        BackgroundMusic = GetComponent<AudioSource>();
           
        OnPause();
    }

    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("Musicas");

        if (musicObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void OnPause()
    {
        if (SceneManager.GetActiveScene().name.Contains("RaceTutorial"))
        {
            BackgroundMusic.Pause();
        }
        else
        {
            BackgroundMusic.Play();
        }
    }
}
