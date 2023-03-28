using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacersController : MonoBehaviour
{
    private void Start()
    {
        CartGameSettings[] racers = GetComponentsInChildren<CartGameSettings>();
        RaceStorage.Instance.SetRacers(racers);

        foreach (var racer in racers)
        {
            RaceStorage.Instance.UpdateCurrentLapByRacer(racer.GetPlayerId());
        }
    }
}
