using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Continue : MonoBehaviour
{
    private static Music_Continue instance;
    public AudioSource BackgroundMusic;
    public AudioClip trackBG;
    
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
