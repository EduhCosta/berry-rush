using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodiumActions : MonoBehaviour
{
    // Actions
    public static Action<List<Podium>> PodiumUpdate;

    public List<Podium> _positions;

    private void FixedUpdate()
    {
        _positions = PodiumStore.Instance.positions;
        if (_positions.Count > 0) { 
            PodiumUpdate(_positions);
        }
    }
}
