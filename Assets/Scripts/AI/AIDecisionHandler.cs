using System.Collections;
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
    public bool HasOstablesOnTheRoad;
    public int DirectionToAvoidObstacle = 0;

    private Queue<RaceCheckpoint> _checkpoints;
    private string _playerId;
    private int _currentLap;
    private LapHandler _lapHandler;

    private void Start()
    {
        _checkpoints = RaceStorage.Instance.GetRaceCheckpoints();
        _playerId = AIIdentifier.GetAIId(gameObject);

        StartCoroutine(CheckingObstacles(0.2f));
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

        //Debug.Log($"{AIIdentifier.GetName(gameObject)} [{_currentLap}] - {indexOfNextCheckpoint}; {_checkpoints.Count}");

        Vector3 nextCheckpointForward =
            _checkpoints.Count > 0 && racerCheckpoints.Count % _checkpoints.Count == 0 ? // If the cart pass on checkpoint
                _lapHandler.gameObject.transform.forward : // The cart shuold go to lapHandler element 
                _checkpoints.ToArray()[indexOfNextCheckpoint].forward; // Case false, going to next checkpoint

        float angle = Vector3.SignedAngle(nextCheckpointForward, transform.forward, Vector3.up);

        AngleToNextCheckpointForward = angle;
    }

    private IEnumerator CheckingObstacles(float interval)
    {
        yield return new WaitForSeconds(interval); //Time to keep sorting
        //Debug.Log("Running");
        
        bool hasObstacleOnRight = DrawRaycast(25, 15);
        bool hasObstacleOnLeft = DrawRaycast(-25, 15);
        bool hasObstacleOnMiddle = DrawRaycast(0, 10);

        //Debug.Log($"Collision on right - {hasObstacleOnRight}");
        //Debug.Log($"Collision on left - {hasObstacleOnLeft}");
        //Debug.Log($"Collision on middle - {hasObstacleOnMiddle}");

        HasOstablesOnTheRoad = hasObstacleOnLeft || hasObstacleOnMiddle || hasObstacleOnRight;
        if (hasObstacleOnMiddle) DirectionToAvoidObstacle = Random.Range(-1, 1);
        if (hasObstacleOnLeft) DirectionToAvoidObstacle = 1;
        if (hasObstacleOnRight) DirectionToAvoidObstacle = -1;

        StartCoroutine(CheckingObstacles(interval));
    }

    private bool DrawRaycast(float angle, float projectionScale) {

        RaycastHit hit;
        Vector3 radar = Quaternion.Euler(0, angle, 0) * transform.forward;
        bool hasObstacleOnAngle = Physics.Raycast(transform.position, radar * projectionScale, out hit, projectionScale, ObstacleMask);

        Debug.DrawRay(transform.position, radar * projectionScale, Color.green);
        return hasObstacleOnAngle;
    }
}
