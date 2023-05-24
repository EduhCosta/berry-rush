using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePodiumPosition : MonoBehaviour
{
    [SerializeField] private int _position = 1;
    [SerializeField] private KartSelector _selector;

    private void Start()
    {
        Podium podium = PodiumStore.Instance.GetPodiumByPosition(_position);
        _selector.SetId(podium.cart._kartSelected);
    }
}
