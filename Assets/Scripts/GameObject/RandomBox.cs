using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBox : MonoBehaviour
{
    // Actions
    public static Action<GameObject, int> HittingBox;

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerIdentifier.IsPlayer(other)) // ToDo: IA getting powerups
        {
            int position = PodiumStore.Instance.GetCurrentPosition(PlayerIdentifier.GetCartGameSettings(other).GetPlayerId());
            HittingBox(PlayerIdentifier.GetCartGameSettings(other).gameObject, position);
        }
    }

}
