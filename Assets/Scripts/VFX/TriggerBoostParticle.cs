using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoostParticle : MonoBehaviour
{
    [SerializeField] private GameObject _particles;
    
    private SphereCartController _cart;

    private void Start()
    {
        _cart = GetComponent<SphereCartController>();
    }

    private void Update()
    {
        if (_cart.CurrentSpeed > 101) _particles.SetActive(true);
        else _particles.SetActive(false);
    }
}
