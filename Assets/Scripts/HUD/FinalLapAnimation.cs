using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLapAnimation : MonoBehaviour
{
    private void Update()
    {
        Debug.Log(RaceStorage.Instance.IsLastLap());
    }
}
