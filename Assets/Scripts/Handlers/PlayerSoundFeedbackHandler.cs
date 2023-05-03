using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundFeedbackHandler : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private AudioSource _audioSource;

    [Header("Feedbacks")]
    [SerializeField] private AudioClip _idle;
    [SerializeField] private AudioClip _accelerate;
    [SerializeField] private AudioClip _break;

    public void OnEnable()
    {
        PlayerInputControllerActions.Accelerate += OnAccelerate;
        PlayerInputControllerActions.Breaking += OnBreaking;
    }

    public void OnDisable()
    {
        PlayerInputControllerActions.Accelerate -= OnAccelerate;
        PlayerInputControllerActions.Breaking -= OnBreaking;
    }

    private void Start()
    {
        _audioSource.clip = _idle;
        _audioSource.Play();
    }

    private void OnBreaking(bool state)
    {
        if (state)
        {
            _audioSource.clip = _break;
            _audioSource.Play();
        }
        else
        {
            _audioSource.clip = _accelerate;
            _audioSource.Play();
        }
    }

    private void OnAccelerate(float accelerateValue)
    {
        if (accelerateValue > 0)
        {
            _audioSource.clip = _accelerate;
            _audioSource.Play();
        } 
        else
        {
            _audioSource.clip = _idle;
            _audioSource.Play();
        }
    }
}
