using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RacersController : MonoBehaviour
{
    private List<int> players = new List<int> { 1, 2, 3, 4 };

    CartGameSettings[] racers;

    private void Start()
    {
        racers = GetComponentsInChildren<CartGameSettings>();
        RaceStorage.Instance.SetRacers(racers);

        foreach (var racer in racers)
        {
            RaceStorage.Instance.UpdateCurrentLapByRacer(racer.GetPlayerId());
        }

        UniquePlayer();
    }

    private void UniquePlayer()
    {
        
        players.Remove(LocalStorage.GetSelectedCharacter(1));
        int i = 0;
        foreach (var racer in racers)
        {
            if (AIIdentifier.IsAI(racer.gameObject))
            {
                racer._kartSelected = players.ElementAt(i);
                racer.gameObject.GetComponentInChildren<KartSelector>().CreateKart(players.ElementAt(i));
                i++;
            }
        }
    }
}
