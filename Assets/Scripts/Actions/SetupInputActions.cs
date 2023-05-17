using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;

public class SetupInputActions : MonoBehaviour
{
    
    [Header("Joystick Inputs")]
    [SerializeField] public InputActionAsset actions;
    [SerializeField] public Sprite ImageKeyboard;
    [SerializeField] public Sprite ImageJoystick;
    Image image;

    void Start()
    {
        image = gameObject.GetComponent<Image>();
        image.sprite = ImageJoystick;
    }

    void Awake()
    {
        actions.FindActionMap("Keyboard").FindAction("Keyboard").performed += OnKeyboard;
        actions.FindActionMap("Joystick").FindAction("Joystick").performed += OnJoystick;
    }

    public void OnEnable()
    {
        actions.FindActionMap("Keyboard").Enable();
        actions.FindActionMap("Joystick").Enable();
        
    }

    public void OnDisable()
    {
        actions.FindActionMap("Keyboard").Disable();
        actions.FindActionMap("Joystick").Disable();
    }

    private void OnKeyboard(InputAction.CallbackContext context)
    {
        image.sprite = ImageKeyboard;
    }

    private void OnJoystick(InputAction.CallbackContext context)
    {
        image.sprite = ImageJoystick;
    }

}
