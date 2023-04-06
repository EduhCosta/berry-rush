using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ButtonSound : MonoBehaviour
{
    public string soundName;

    public void PlaySound()
    {
        SoundManager.instance.Play(soundName);
    }
}
