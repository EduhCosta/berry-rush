using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTime : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
