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

            DeadZoneHandler_OutOfTrack(racerId);

            CheckpointData[] checkpoints = RaceStorage.Instance.GetCheckpointsByRacer(racerId).ToArray();
            if (checkpoints.Length < 0) return;
            
            CheckpointData lastCheckpoint =  checkpoints[checkpoints.Length - 1];
            Debug.Log(lastCheckpoint.ToString());

            RaceCheckpoint raceCheckpoint = RaceStorage.Instance.GetRaceCheckpointsById(lastCheckpoint.checkpointOrder);
            Debug.Log(raceCheckpoint.gameObject.transform);

            if (PlayerIdentifier.IsPlayer(other))
            {
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
        Debug.Log("Call me"); 
        Debug.Log(_lastTransform.position);

        GameObject cart = null;
        Collider collider = null;
        if (_isPlayer) cart = _currentDead.GetComponentInChildren<SphereCartController>().gameObject;
        if (_isAi) cart = _currentDead.GetComponentInChildren<AICartController>().gameObject;

        collider = _currentDead.GetComponentInChildren<Collider>();

        if (cart == null || collider == null) return;

        Vector3 newPosition = new Vector3(_lastTransform.position.x, )

        cart.transform.position = _lastTransform.position;
        collider.transform.position = _lastTransform.position;

        // Transform[] transforms = _currentDead.GetComponentsInChildren<Transform>();
        // foreach (Transform t in transforms) t.position = Vector3.zero;

        _lastTransform = null;
        _currentDead = null;
        _isAi = false;
        _isPlayer = false;
    }
}
