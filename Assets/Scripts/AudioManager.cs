using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    float masterVolume;

    private bool audioOn = true;

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
    }
}
