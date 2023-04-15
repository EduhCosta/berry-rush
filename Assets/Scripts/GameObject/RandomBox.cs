using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RandomBox : MonoBehaviour
{
    // Actions
    public static Action<GameObject, int> HittingBox;
    public static Action<int> HittingInstanceBox;

    private float _timer = 10; // sec

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerIdentifier.IsPlayer(other)) // ToDo: IA getting powerups
        {
            int position = PodiumStore.Instance.GetCurrentPosition(PlayerIdentifier.GetCartGameSettings(other).GetPlayerId());
            HittingBox(PlayerIdentifier.GetCartGameSettings(other).gameObject, position);
            HittingInstanceBox(gameObject.GetInstanceID());
        }
    }

    private void Update()
    {
        if (_timer > 0 && gameObject.activeSelf == false)
        {
            _timer -= Time.deltaTime;
        } 
        else
        {
            gameObject.SetActive(true);
            _timer = 10;
        }
    }

}
