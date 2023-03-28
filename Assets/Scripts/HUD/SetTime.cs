using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetTime : MonoBehaviour
{
    [SerializeField] public TMP_Text Time;

    void FixedUpdate()
    {
        float time = RaceStorage.Instance.GetCurrentTime();
        Time.text = $"Tempo da corrida: {time}";
    }
}
