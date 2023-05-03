using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    // Toggle fullscreen
    [SerializeField] Button tgl_FullScreenOn;
    [SerializeField] Button tgl_FullScreenOff;

    // Resolutions
    [SerializeField] TMP_Dropdown sel_Resolutions;

    // Audio
    [SerializeField] Slider sld_GeneralVolume;
    [SerializeField] AudioMixer amix_General;

    [SerializeField] Slider sld_MusicVolume;
    [SerializeField] Slider sld_SFXVolume;
    [SerializeField] Slider sld_VoicesVolume;

    private Resolution[] supportedResolutions;
    private bool isFullScreen = false;

    private const string GENERAL_VOLUME = "MasterVolume";
    private const string MUSIC_VOLUME = "MusicVolume";
    private const string SFX_VOLUME = "SFXVolume";
    private const string VOICES_VOLUME = "VoicesVolume";

    private void Start()
    {
        isFullScreen = Screen.fullScreen;
        supportedResolutions = Screen.resolutions;
        this.GetAllResolutions(supportedResolutions);

        this.WarmUpGame();
    }

    private void Update()
    {
        this.SetFullScreenSelector();
    }

    private void OnEnable()
    {
        tgl_FullScreenOn.onClick.AddListener(this.ToggleFullScreen);
        tgl_FullScreenOff.onClick.AddListener(this.ToggleFullScreen);
        sel_Resolutions.onValueChanged.AddListener(this.SetResolution);
        sld_GeneralVolume.onValueChanged.AddListener(this.OnChangeGeneralVolume);
        sld_MusicVolume.onValueChanged.AddListener(this.OnChangeMusicVolume);
        sld_SFXVolume.onValueChanged.AddListener(this.OnChangeSFXVolume);
        sld_VoicesVolume.onValueChanged.AddListener(this.OnChangeVoicesVolume);
    }

    private void OnDisable()
    {
        tgl_FullScreenOn.onClick.RemoveListener(this.ToggleFullScreen);
        tgl_FullScreenOff.onClick.RemoveListener(this.ToggleFullScreen);
        sel_Resolutions.onValueChanged.RemoveListener(this.SetResolution);
        sld_GeneralVolume.onValueChanged.RemoveListener(this.OnChangeGeneralVolume);
        sld_MusicVolume.onValueChanged.RemoveListener(this.OnChangeMusicVolume);
        sld_SFXVolume.onValueChanged.RemoveListener(this.OnChangeSFXVolume);
        sld_VoicesVolume.onValueChanged.RemoveListener(this.OnChangeVoicesVolume);
    }

    private void WarmUpGame()
    {
        // General Volume
        float generalVolume = LocalStorage.GetGeneralVolume(0.6f);
        float generalVolumeUpdated = this.OnChangeVolume(GENERAL_VOLUME, generalVolume);
        LocalStorage.SetGeneralVolume(generalVolumeUpdated);
        sld_GeneralVolume.SetValueWithoutNotify(generalVolume);

        // Music Volume
        float musicVolume = LocalStorage.GetMusicVolume(0.6f);
        float musicVolumeUpdated = this.OnChangeVolume(MUSIC_VOLUME, musicVolume);
        LocalStorage.SetMusicVolume(musicVolumeUpdated);
        sld_MusicVolume.SetValueWithoutNotify(musicVolume);

        // SFX Volume
        float sfxVolume = LocalStorage.GetSFXVolume(0.6f);
        float sfxVolumeUpdated = this.OnChangeVolume(SFX_VOLUME, sfxVolume);
        LocalStorage.SetSFXVolume(sfxVolumeUpdated);
        sld_SFXVolume.SetValueWithoutNotify(sfxVolume);

        // Voices Volume
        float voicesVolume = LocalStorage.GetVoicesVolume(0.6f);
        float voicesVolumeUpdated = this.OnChangeVolume(VOICES_VOLUME, voicesVolume);
        LocalStorage.SetVoicesVolume(voicesVolumeUpdated);
        sld_VoicesVolume.SetValueWithoutNotify(voicesVolume);

        // Resolution
        int resolution = LocalStorage.GetResolution(supportedResolutions.Length - 1);
        this.SetResolution(resolution);
        sel_Resolutions.SetValueWithoutNotify(resolution);

        // FullScreen
        this.isFullScreen = LocalStorage.GetFullScreen(true);
        Screen.fullScreen = this.isFullScreen;
    }

    public void OnChangeGeneralVolume(float value)
    {
        float calculatedVolume = Mathf.Log10(value) * 20;
        amix_General.SetFloat(GENERAL_VOLUME, calculatedVolume);

        LocalStorage.SetGeneralVolume(value);
    }

    public void OnChangeMusicVolume(float value)
    {
        float calculatedVolume = Mathf.Log10(value) * 20;
        amix_General.SetFloat(MUSIC_VOLUME, calculatedVolume);

        LocalStorage.SetMusicVolume(value);
    }

    public void OnChangeSFXVolume(float value)
    {
        float calculatedVolume = Mathf.Log10(value) * 20;
        amix_General.SetFloat(SFX_VOLUME, calculatedVolume);

        LocalStorage.SetSFXVolume(value);
    }

    public void OnChangeVoicesVolume(float value)
    {
        float calculatedVolume = Mathf.Log10(value) * 20;
        amix_General.SetFloat(VOICES_VOLUME, calculatedVolume);

        LocalStorage.SetVoicesVolume(value);
    }

    private float OnChangeVolume(string exposedParamName, float value)
    {
        float calculatedVolume = Mathf.Log10(value) * 20;
        amix_General.SetFloat(exposedParamName, calculatedVolume);

        return value;
    }

    private void GetAllResolutions(Resolution[] resList)
    {
        List<string> resolutionList = new List<string>();

        foreach (Resolution resolution in resList)
        {
            resolutionList.Add($"{resolution.width}x{resolution.height} ({resolution.refreshRate}Hz)");
        }

        sel_Resolutions.ClearOptions(); // Limpa op??es residuais no dropdown
        sel_Resolutions.AddOptions(resolutionList);
    }

    public void SetResolution(int selectedIndex)
    {
        Resolution currestRes = supportedResolutions[selectedIndex];

        Screen.SetResolution(currestRes.width, currestRes.height, Screen.fullScreenMode, currestRes.refreshRate);

        LocalStorage.SetResolution(selectedIndex);
    }

    public void ToggleFullScreen()
    {
        bool newState = !this.isFullScreen;
        Screen.fullScreen = newState;
        this.isFullScreen = newState;
        this.SetFullScreenSelector();

        LocalStorage.SetFullScreen(newState);
    }

    private void SetFullScreenSelector()
    {
        if (Screen.fullScreen)
        {
            tgl_FullScreenOn.interactable = false;
            tgl_FullScreenOff.interactable = true;
        }
        else
        {
            tgl_FullScreenOn.interactable = true;
            tgl_FullScreenOff.interactable = false;
        }
    }
}
