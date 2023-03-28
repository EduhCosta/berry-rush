using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodiumActions : MonoBehaviour
{
    // Actions
    public static Action<List<Podium>> PodiumUpdate;

    private List<Podium> _positions;

    private void Update()
    {
        _positions = GetComponent<PodiumStore>().positions;
        PodiumUpdate(_positions);
    }
}
