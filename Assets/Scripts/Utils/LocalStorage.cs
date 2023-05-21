using UnityEngine;

public class LocalStorage
{
    const string GENERAL_VOLUME = "@BERRYRUSH_GENERAL_VOLUME";
    const string MUSIC_VOLUME = "@BERRYRUSH_MUSIC_VOLUME";
    const string SFX_VOLUME = "@BERRYRUSH_SFX_VOLUME";
    const string VOICES_VOLUME = "@BERRYRUSH_VOICES_VOLUME";
    const string RESOLUTION = "@BERRYRUSH_RESOLUTION";
    const string FULL_SCREEN = "@BERRYRUSH_FULL_SCREEN";
    const string SELECTED_CHAR = "@BERRYRUSH_SELECTED_CHAR";
    const string INPUT_ACTIONS = "@BERRYRUSH_INPUT_ACTIONS";

    #region GeneralVolume
    public static void SetGeneralVolume(float currentValue)
    {
        PlayerPrefs.SetFloat(GENERAL_VOLUME, currentValue);
    }

    public static float GetGeneralVolume(float defaultValue)
    {
        return PlayerPrefs.GetFloat(GENERAL_VOLUME, defaultValue);
    }
    #endregion

    #region MusicVolume
    public static void SetMusicVolume(float currentValue)
    {
        PlayerPrefs.SetFloat(MUSIC_VOLUME, currentValue);
    }

    public static float GetMusicVolume(float defaultValue)
    {
        return PlayerPrefs.GetFloat(MUSIC_VOLUME, defaultValue);
    }
    #endregion

    #region SFXVolume
    public static void SetSFXVolume(float currentValue)
    {
        PlayerPrefs.SetFloat(SFX_VOLUME, currentValue);
    }

    public static float GetSFXVolume(float defaultValue)
    {
        return PlayerPrefs.GetFloat(SFX_VOLUME, defaultValue);
    }
    #endregion

    #region VoicesVolume
    public static void SetVoicesVolume(float currentValue)
    {
        PlayerPrefs.SetFloat(VOICES_VOLUME, currentValue);
    }

    public static float GetVoicesVolume(float defaultValue)
    {
        return PlayerPrefs.GetFloat(VOICES_VOLUME, defaultValue);
    }
    #endregion

    #region Resolution
    public static void SetResolution(int currentIndex)
    {
        PlayerPrefs.SetInt(RESOLUTION, currentIndex);
    }

    public static int GetResolution(int defaultValue)
    {
        return PlayerPrefs.GetInt(RESOLUTION, defaultValue);
    }
    #endregion

    #region FullScreen
    public static void SetFullScreen(bool currentValue)
    {
        PlayerPrefsX.SetBool(FULL_SCREEN, currentValue);
    }

    public static bool GetFullScreen(bool defaultValue)
    {
        return PlayerPrefsX.GetBool(FULL_SCREEN, defaultValue);
    }
    #endregion

    #region SelectedCharacter
    public static void SetSelectedCharacter(int currentValue)
    {
        PlayerPrefs.SetInt(SELECTED_CHAR, currentValue);
    }

    public static int GetSelectedCharacter(int defaultValue)
    {
        return PlayerPrefs.GetInt(SELECTED_CHAR, defaultValue);
    }
    #endregion

    #region IsUsingKeyboard
    public static void SetIsUsingKeyboard(bool currentValue)
    {
        PlayerPrefsX.SetBool(INPUT_ACTIONS, currentValue);
    }

    public static bool GetIsUsingKeyboard(bool defaultValue)
    {
        return PlayerPrefsX.GetBool(INPUT_ACTIONS, defaultValue);
    }
    #endregion
}