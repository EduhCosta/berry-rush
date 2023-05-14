using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetAudio : MonoBehaviour
{
    private const string GENERAL_VOLUME = "MasterVolume";
    private const string MUSIC_VOLUME = "MusicVolume";
    private const string SFX_VOLUME = "SFXVolume";
    private const string VOICES_VOLUME = "VoicesVolume";

    [SerializeField] AudioMixer amix_General;

    private void Start()
    {
        // General Volume
        float generalVolume = LocalStorage.GetGeneralVolume(0.6f);
        OnChangeVolume(GENERAL_VOLUME, generalVolume);

        // Music Volume
        float musicVolume = LocalStorage.GetMusicVolume(0.6f);
        OnChangeVolume(MUSIC_VOLUME, musicVolume);

        // SFX Volume
        float sfxVolume = LocalStorage.GetSFXVolume(0.6f);
        OnChangeVolume(SFX_VOLUME, sfxVolume);

        // Voices Volume
        float voicesVolume = LocalStorage.GetVoicesVolume(0.6f);
        OnChangeVolume(VOICES_VOLUME, voicesVolume);
    }

    public void OnChangeVolume(string id, float value)
    {
        float calculatedVolume = Mathf.Log10(value) * 20;
        amix_General.SetFloat(id, calculatedVolume);
    }
}
