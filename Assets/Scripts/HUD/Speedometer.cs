using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Speedometer : MonoBehaviour
{
    [SerializeField] SphereCartController kart;
    [SerializeField] public TMP_Text velocimetro;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        velocimetro.text = kart.CurrentSpeed.ToString(@"00");
    }
}
