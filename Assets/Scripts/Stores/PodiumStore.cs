using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PodiumStore : MonoBehaviour
{
    public List<Podium> positions = new();
    public static PodiumStore Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("More than one instance of a PodiumStore singleton");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        CheckpointActions.SetCartPosition += UpdatePosition;
    }

    private void OnDisable()
    {
        CheckpointActions.SetCartPosition -= UpdatePosition;
    }

    private void UpdatePosition(CartGameSettings cart, int checkpointOrder)
    {
        float timeStamp = RaceStorage.Instance.GetCurrentTime();
        LapData currentLap = RaceStorage.Instance.GetCurrentLapByRacer(cart.GetPlayerId());

        // Remove item if exists on list
        var itemToRemove = positions.SingleOrDefault(c => c.cart.GetInstanceID() == cart.GetInstanceID());
        if (itemToRemove != null)
        {
            positions.Remove(itemToRemove);
        }

        Podium newPosition = new Podium(cart, checkpointOrder, timeStamp, currentLap);
        positions.Add(newPosition);

        positions.Sort(SortPodiumList);
    }

    private int SortPodiumList(Podium before, Podium after)
    {
        int tmpSort = after.currentLap.lapDone.CompareTo(before.currentLap.lapDone);
        tmpSort = tmpSort == 0 ? after.checkpointOrder.CompareTo(before.checkpointOrder) : tmpSort;
        tmpSort = tmpSort == 0 ? tmpSort += before.timeStamp.CompareTo(after.timeStamp) : tmpSort;

        return tmpSort;
    }

    public int GetCurrentPosition(string playerId)
    {
        return positions.FindIndex(position => position.cart.GetPlayerId() == playerId) + 1;
    }

    public GameObject GetPlayerByPosition(int position)
    {
        return positions[position - 1].cart.gameObject;
    }
}
