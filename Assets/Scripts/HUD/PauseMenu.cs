using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

public class PauseMenu : MonoBehaviour
{
    public Transform pauseMenu;
    [SerializeField] public GameObject eSystem;
    [Header("Actions")]
    [SerializeField] public InputActionAsset Actions;

    void Awake()
    {
        Actions.FindActionMap("Gameplay").FindAction("JumpCutscene").performed += OnPause;
    }

    public void OnEnable()
    {
        Actions.FindActionMap("Gameplay").Enable();
    }

    public void OnDisable()
    {
        Actions.FindActionMap("Gameplay").Disable();
    }

    void OnPause(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().name.Contains("Race")){
                if (pauseMenu.gameObject.activeSelf)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
        }
    }

    public void Resume()
    {
        eSystem.gameObject.SetActive(true);
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void Pause()
    {
        eSystem.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }



}
