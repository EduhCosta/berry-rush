using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneHandler : MonoBehaviour
{
    public static Action<string> DeadZoneHandler_OutOfTrack;

    private GameObject _currentDead; 
    private Transform _lastTransform = null;
    private bool _isPlayer = false;
    private bool _isAi = false;

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerIdentifier.IsPlayer(other) || AIIdentifier.IsAI(other))
        {
            CartGameSettings cart = other.GetComponentInParent<CartGameSettings>();
            string racerId = cart.GetPlayerId();

            if (AIIdentifier.IsAI(other)) DeadZoneHandler_OutOfTrack(racerId);

            CheckpointData[] checkpoints = RaceStorage.Instance.GetCheckpointsByRacer(racerId).ToArray();
            if (checkpoints.Length < 0) return; // ToDo: Devemos definir a posição do carro para o início da pista nesse caso
            
            CheckpointData lastCheckpoint =  checkpoints[checkpoints.Length - 1];
            //Debug.Log(lastCheckpoint.ToString());

            RaceCheckpoint raceCheckpoint = RaceStorage.Instance.GetRaceCheckpointsById(lastCheckpoint.checkpointOrder);

            if (PlayerIdentifier.IsPlayer(other))
            {
                //Debug.Log("Is player");
                _isPlayer = true;
                _currentDead = PlayerIdentifier.GetPlayerGameObject(other);
            }
            if (AIIdentifier.IsAI(other))
            {
                _isAi = true;
                _currentDead = AIIdentifier.GetAIGameObject(other);
            }

            _lastTransform = raceCheckpoint.gameObject.transform;
            Reborn();
        }
    }

    private void Reborn()
    {
        //Debug.Log(_lastTransform.position);

        GameObject cart = null;
        GameObject collider = null;
        if (_isPlayer) cart = _currentDead.GetComponentInChildren<SphereCartController>().gameObject;
        if (_isAi) cart = _currentDead.GetComponentInChildren<AICartController>().gameObject;

        collider = _currentDead.GetComponentInChildren<SphereCollider>().gameObject;

        if (cart == null || collider == null) return;

        // Debug.Log(_lastTransform.position);

        cart.transform.position = _lastTransform.position;
        collider.transform.position = _lastTransform.position;
        cart.transform.forward = _lastTransform.forward;
        collider.transform.forward = _lastTransform.forward;

        // Freezing Kart
        if (_isPlayer) cart.GetComponent<SphereCartController>().OnStun(1);
        if (_isAi) cart.GetComponent<AICartController>().OnStun(1);

        _lastTransform = null;
        _currentDead = null;
        _isAi = false;
        _isPlayer = false;
    }
}
