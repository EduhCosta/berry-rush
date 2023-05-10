using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetPositions : MonoBehaviour
{
    [SerializeField] public TMP_Text CurrentPosition;
    [SerializeField] public TMP_Text PositionIndication;
    [SerializeField] public GameObject panel;

    private List<Podium> _positions = new();
    private bool _isTimerTrigged = false;
    private float _timer = 0;

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
        for(int i = 0; i < _positions.Count; i++)
        {
            if (_positions.Count > 3) // ToDo: Change to size of racers list
            {
                if (PlayerIdentifier.IsPlayer(_positions[i].cart.gameObject))
                {
                    int position = i + 1;
                    string idetificator = "";
                    if (position == 1)
                    {
                        idetificator = "st";
                    }
                    if (position == 2)
                    {
                        idetificator = "nd";
                    }
                    if (position == 3)
                    {
                        idetificator = "rd";
                    }
                    if (position == 4)
                    {
                        idetificator = "th";
                    }

                    CurrentPosition.text = $"{i + 1} ";
                    PositionIndication.text = idetificator;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (_isTimerTrigged)
        {
            _timer += Time.fixedDeltaTime;
        }

        if (_timer > 3)
        {
            RaceStorage.Instance.EndGame();
            SceneManager.LoadScene("PodiumTutorial");
        }
    }

    private void UpdatePodiumPosition(List<Podium> obj)
    {
        _positions = obj;
    }

    private void EndRace()
    {
        panel.SetActive(true);
        _isTimerTrigged = true;
    }

}
