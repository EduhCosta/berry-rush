using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostTrack : MonoBehaviour
{
    [Tooltip("Value to add velocity to kart")]
    [SerializeField] float BoostValue;

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerIdentifier.IsPlayer(other))
        {
            SphereCartController cart = PlayerIdentifier.GetKart(other);
            cart.OnBoost(BoostValue);
        }

        if (AIIdentifier.IsAI(other))
        {
            AICartController cart = AIIdentifier.GetAIKart(other);
            cart.OnBoost(BoostValue);
        }
    }
}