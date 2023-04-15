using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBoxController : MonoBehaviour
{

    [SerializeField] public float TimeToRespawn = 10;
    [SerializeField] public GameObject box;

    private bool _isCounting = false;
    private float _timer = 0;

    private void OnEnable()
    {
        RandomBox.HittingInstanceBox += OnHit;
    }

    private void OnDisable()
    {
        RandomBox.HittingInstanceBox -= OnHit;
    }

    private void Start()
    {
        _timer = TimeToRespawn;
    }

    private void Update()
    {
        if (_isCounting)
        {
            if (_timer > 0 && box.activeSelf == false)
            {
                _timer -= Time.deltaTime;
            }
            else
            {
                box.SetActive(true);
                _isCounting = false;
                _timer = TimeToRespawn;
            }
        }
    }

    private void OnHit(int instanceID)
    {
        if (instanceID == box.GetInstanceID())
        {
            box.SetActive(false);
            _isCounting = true;
        }
    }
}
