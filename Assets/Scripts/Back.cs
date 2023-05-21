using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour
{
    [Tooltip("Nome da cena que esse botão irá navegar")]
    [SerializeField] string SceneName;

    [Header("Joystick Inputs")]
    [SerializeField] public InputActionAsset Actions;

    void Awake()
    {
        Actions.FindActionMap("UI").FindAction("Back").performed += OnBack;
    }

    public void OnEnable()
    {
        Actions.FindActionMap("UI").Enable();
    }

    public void OnDisable()
    {
        Actions.FindActionMap("UI").Disable();
    }
        
    void OnBack(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneName);
    }
}
