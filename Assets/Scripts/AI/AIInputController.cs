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
    private float _directionToAvoidObstacle = 0f;

    public float Direction = 0;
    public float Accelerate = 0;
    public bool IsDrifting = false;

    private CheckpointData[] _aICheckpointsDone;
    private int currentCheckpoint = 1;
    private bool _restarting = false;
    private AIDecisionHandler _handler;

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

    private void Start()
    {
        _handler = GetComponent<AIDecisionHandler>();
    }

    void Update()
    {
        _aICheckpointsDone = RaceStorage.Instance.GetCheckpointsByRacer(_aiId).ToArray();
        
        _forwardAngleToNextCheckpoint = _handler.AngleToNextCheckpointForward;
        _hasOstablesOnTheRoad = _handler.HasOstablesOnTheRoad;
        _directionToAvoidObstacle = _handler.DirectionToAvoidObstacle;

        Accelerate = _handler.IsMustRollback ? -1 : _restarting ? 0 : GoToNextPoint();
        Direction = _hasOstablesOnTheRoad ? _directionToAvoidObstacle * 0.5f : KeepDirectionToNextCheckpoint();
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
