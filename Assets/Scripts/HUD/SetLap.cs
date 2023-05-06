using TMPro;
using UnityEngine;

public class SetLap : MonoBehaviour
{
    [SerializeField] public TMP_Text LapValue;
    [SerializeField] public CartGameSettings Player;

    void FixedUpdate()
    {
        LapData currentLap = RaceStorage.Instance.GetCurrentLapByRacer(Player.GetPlayerId());
        int totalLaps = RaceStorage.Instance.GetTotalRaceLap();
        LapValue.text = $"{currentLap.lapDone}/{totalLaps}";
    }
}
