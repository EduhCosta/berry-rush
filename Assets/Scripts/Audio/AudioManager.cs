using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public float masterVolume, fxVolume, musicaVolume;
    public Slider masterSlider, fxSlider, musicaSlider;

    private bool audioOn = true;

    void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("Master");
        fxSlider.value = PlayerPrefs.GetFloat("FX");
        // musicaSlider.value = PlayerPrefs.GetFloat("Musica");
    }
    
    public void VolumeGame()
    {
        audioOn = !audioOn;
        if(audioOn == true)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }

    public void MasterVolume(float volume)
    {
        masterVolume = volume;
        AudioListener.volume = masterVolume;

        PlayerPrefs.SetFloat("Master", masterVolume);
    }
    
    public void FxVolume(float volume)
    {
        fxVolume = volume;
        GameObject[] Fxs = GameObject.FindGameObjectsWithTag("SoundManager");
        for (int i=0; i<Fxs.Length; i++)
        {
            Fxs[i].GetComponent<AudioSource>().volume = fxVolume;
        }

        PlayerPrefs.SetFloat("FX", fxVolume);
    }
    
    public void MusicaVolume(float volume)
    {
        musicaVolume = volume;
        AudioListener.volume = musicaVolume;

        PlayerPrefs.SetFloat("Musica", musicaVolume);
    }
}
