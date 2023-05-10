using TMPro;
using UnityEngine;

public class SetLap : MonoBehaviour
{
    [SerializeField] public TMP_Text TotalLaps;
    [SerializeField] public TMP_Text CurrentLap;
    [SerializeField] public CartGameSettings Player;

    void FixedUpdate()
    {
        LapData currentLap = RaceStorage.Instance.GetCurrentLapByRacer(Player.GetPlayerId());
        int totalLaps = RaceStorage.Instance.GetTotalRaceLap();
        TotalLaps.text = totalLaps.ToString();
        CurrentLap.text = currentLap.lapDone.ToString();
    }
}
