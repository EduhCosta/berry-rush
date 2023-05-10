using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetTime : MonoBehaviour
{
    [SerializeField] public TMP_Text Time;

    void FixedUpdate()
    {
        float seconds = RaceStorage.Instance.GetCurrentTime();
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        Time.text = time.ToString(@"mm\:ss\:ff");
    }
}
