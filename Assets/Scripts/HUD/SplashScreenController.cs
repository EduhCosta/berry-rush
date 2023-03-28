using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{
    [SerializeField] string NextScreen;
    [Tooltip("Tempo para seguir para próxima tela")]
    [SerializeField] float TimeToGo = 3;

    private float _timeCounter = 0;

    private void Start()
    {
        _timeCounter = 0;
    }

    void Update()
    {
        _timeCounter += Time.deltaTime;
        // Jump to next screen after 3sec
        if (_timeCounter > TimeToGo)
        {
            SceneManager.LoadScene(NextScreen);
        }
    }
}
