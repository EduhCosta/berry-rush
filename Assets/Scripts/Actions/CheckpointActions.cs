using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointActions : MonoBehaviour
{
    // Actions
    public static Action<CartGameSettings, int> SetCartPosition;

    private int CheckpointOrder = 0;

    private void Start()
    {
        CheckpointOrder = GetComponent<Checkpoint>().CheckpointOrder;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerIdentifier.IsPlayer(other) || AIIdentifier.IsAI(other))
        {
            CartGameSettings cart = other.GetComponentInParent<CartGameSettings>();
            SetCartPosition(cart, CheckpointOrder);
        }
    }
}
