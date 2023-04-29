using System;
using UnityEngine;

public class AIInputController : MonoBehaviour
{
    const int TURN_LEFT = -1;
    const int TURN_RIGHT = 1;
    const int NEUTRAL = 0;

    private string _aiId;
    private float _forwardAngleToNextCheckpoint;
    private bool _hasOstablesOnTheRoad;
    private int _directionToAvoidObstacle = 0;

    public float Direction = 0;
    public float Accelerate = 0;
    public bool IsDrifting = false;

    private CheckpointData[] _aICheckpointsDone;
    private int currentCheckpoint = 1;
    private bool _restarting = false;

    void OnEnable()
    {
        _aiId = AIIdentifier.GetAIId(gameObject);
        DeadZoneHandler.DeadZoneHandler_OutOfTrack += ShouldRestarting;
    }

    private void ShouldRestarting(string currentId)
    {
        if (_aiId == currentId) _restarting = true;
        else _restarting = false;
    }

    void Update()
    {
        _aICheckpointsDone = RaceStorage.Instance.GetCheckpointsByRacer(_aiId).ToArray();

        AIDecisionHandler handler = GetComponent<AIDecisionHandler>();
        _forwardAngleToNextCheckpoint = handler.AngleToNextCheckpointForward;
        _hasOstablesOnTheRoad = handler.HasOstablesOnTheRoad;
        _directionToAvoidObstacle = handler.DirectionToAvoidObstacle;

        Accelerate = _restarting ? 0 : GoToNextPoint();
        Direction = _hasOstablesOnTheRoad ? _directionToAvoidObstacle : KeepDirectionToNextCheckpoint();
    }

    private int GoToNextPoint()
    {
        if (Array.Exists(_aICheckpointsDone, c => c.checkpointOrder == currentCheckpoint))
        {
            currentCheckpoint++;
        } 
        else
        {
            return  1;
        }

        return 0;
    }

    public int KeepDirectionToNextCheckpoint()
    {
        if (_forwardAngleToNextCheckpoint > 0 && _forwardAngleToNextCheckpoint <= 190)
        {
            return TURN_LEFT;
        }
        else if (_forwardAngleToNextCheckpoint < 0 && _forwardAngleToNextCheckpoint >= -180)
        {
            return TURN_RIGHT;
        }

        return NEUTRAL;
    }

    public void StartDrift()
    {
        IsDrifting = true;
    }

    public void EndDrift()
    {
        IsDrifting = false;
    }
}
