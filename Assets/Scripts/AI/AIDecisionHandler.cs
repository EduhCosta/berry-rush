using System.Collections.Generic;
using UnityEngine;

public class AIDecisionHandler : MonoBehaviour
{
    // Temporary
    [SerializeField] public float AngleToAnalysis = 15f;
    [SerializeField] public float CheckableAheadDistance = 10f;
    [SerializeField] LayerMask ObstacleMask;
    [SerializeField] LayerMask CornerPitMask;

    // Exposed values
    public List<int> BlockedDirections = new();
    public float AngleToNextCheckpointForward = 0;
    public int DirectionToCentralize = 0;

    private Queue<RaceCheckpoint> _checkpoints;
    private string _playerId;
    private int _currentLap;
    private LapHandler _lapHandler;

    private void Start()
    {
        _checkpoints = RaceStorage.Instance.GetRaceCheckpoints();
        _playerId = AIIdentifier.GetAIId(gameObject);
    }

    private void Update()
    {
        if (_checkpoints.Count == 0) _checkpoints = RaceStorage.Instance.GetRaceCheckpoints();
        _currentLap = RaceStorage.Instance.GetCurrentLapByRacer(_playerId).lapDone;
        _lapHandler = RaceStorage.Instance.GetLapHandler();

        AlignNextCheckpointForward(_playerId);
    }

    /// <summary>
    /// Verify the checkpoint passed by AI and verify the diference between AI forward and nextcheckpoint forward
    /// </summary>
    /// <param name="playerId">Cart unique id</param>
    private void AlignNextCheckpointForward(string playerId)
    {
        Queue<CheckpointData> racerCheckpoints = RaceStorage.Instance.GetCheckpointsByRacer(playerId);

        int indexOfNextCheckpoint = racerCheckpoints.Count - (_checkpoints.Count * (_currentLap - 1));

        Debug.Log($"{AIIdentifier.GetName(gameObject)} [{_currentLap}] - {indexOfNextCheckpoint}; {_checkpoints.Count}");

        Vector3 nextCheckpointForward =
            _checkpoints.Count > 0 && racerCheckpoints.Count % _checkpoints.Count == 0 ? // If the cart pass on checkpoint
                _lapHandler.gameObject.transform.forward : // The cart shuold go to lapHandler element 
                _checkpoints.ToArray()[indexOfNextCheckpoint].forward; // Case false, going to next checkpoint

        float angle = Vector3.SignedAngle(nextCheckpointForward, transform.forward, Vector3.up);

        AngleToNextCheckpointForward = angle;
    }
}
