using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CutsceneJumper : MonoBehaviour
{
    [Header("NextSceneProperties")]
    [SerializeField] public string SceneName;

    [Header("Joystick Inputs")]
    [SerializeField] public InputActionAsset Actions;

    void Awake()
    {
        Actions.FindActionMap("Gameplay").FindAction("JumpCutscene").performed += GoToScene;
    }

    public void OnEnable()
    {
        Actions.FindActionMap("Gameplay").Enable();
    }

    public void OnDisable()
    {
        Actions.FindActionMap("Gameplay").Disable();
    }

    void GoToScene(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().name.Contains("Cutscene"))
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
