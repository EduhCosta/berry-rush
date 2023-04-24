using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputControllerActions : MonoBehaviour
{
    public static Action ConsumePowerUp;
    public static Action<float> Accelerate;
    public static Action<float> Steering;
    public static Action<bool> Breaking;

    [Header("Joystick Inputs")]
    [SerializeField] public InputActionAsset actions;

    void Awake()
    {
        actions.FindActionMap("Gameplay").FindAction("Accelerate").performed += OnAccelerate;
        actions.FindActionMap("Gameplay").FindAction("Accelerate").canceled += OnAccelerate;
        actions.FindActionMap("Gameplay").FindAction("SteeringAngle").performed += OnSteering;
        actions.FindActionMap("Gameplay").FindAction("SteeringAngle").canceled += OnSteering;
        actions.FindActionMap("Gameplay").FindAction("BreakAndDrift").performed += OnBreaking;
        actions.FindActionMap("Gameplay").FindAction("BreakAndDrift").canceled += OnBreaking;
        actions.FindActionMap("Gameplay").FindAction("PowerUpTrigger").performed += OnHitPowerUpButton;
    }

    public void OnEnable()
    {
        actions.FindActionMap("Gameplay").Enable();
    }

    public void OnDisable()
    {
        actions.FindActionMap("Gameplay").Disable();
    }

    private void OnHitPowerUpButton(InputAction.CallbackContext obj)
    {
        ConsumePowerUp();
    }

    private void OnAccelerate(InputAction.CallbackContext context)
    {
        if (context.phase.ToString().ToLower() == "performed")
        {
            Accelerate(context.ReadValue<float>());
        }
        else if (context.phase.ToString().ToLower() == "canceled")
        {
            Accelerate(0);
        }

    }
    private void OnSteering(InputAction.CallbackContext context)
    {
        if (context.phase.ToString().ToLower() == "performed")
        {
            Steering(context.ReadValue<float>());
        }
        else if (context.phase.ToString().ToLower() == "canceled")
        {
            Steering(0);
        }
    }

    private void OnBreaking(InputAction.CallbackContext context)
    {
        if (context.phase.ToString().ToLower() == "performed")
        {
            Breaking(true);
        }
        else if (context.phase.ToString().ToLower() == "canceled")
        {
            Breaking(false);
        }
    }
}
