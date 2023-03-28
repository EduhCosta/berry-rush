using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetPositions : MonoBehaviour
{
    [SerializeField] public TMP_Text[] text_fields;
    [SerializeField] public GameObject panel;

    private List<Podium> _positions;

    private void OnEnable()
    {
        PodiumActions.PodiumUpdate += UpdatePodiumPosition;
        LapHandler.EndRace += EndRace;
    }

    private void OnDisable()
    {
        PodiumActions.PodiumUpdate -= UpdatePodiumPosition;
        LapHandler.EndRace -= EndRace;
    }

    void Update()
    {
        for(int i = 0; i < text_fields.Length; i++)
        {
            if (_positions.Count > 3) // ToDo: Change to size of racers list
            {
                text_fields[i].text = _positions[i].cart.PlayerName;
            }
        }
    }

    private void UpdatePodiumPosition(List<Podium> obj)
    {
        _positions = obj;
    }
    private void EndRace()
    {
        panel.SetActive(true);
    }

}
