using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleStore : MonoBehaviour
{
    public static ObstacleStore Instance { get; private set; }

    // Same as contructors in pure C#
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }



}
