using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITrackView : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Checkpoint[] allCheckpoint = GetComponentsInChildren<Checkpoint>();
        DrawPath(allCheckpoint);
        DrawPathPoints(allCheckpoint);
    }

    private void DrawPath(Checkpoint[] raceCheckpoints)
    {
        Gizmos.color = Color.red;
        for (int i = 1; i < raceCheckpoints.Length; i++)
        {
            Gizmos.DrawLine(raceCheckpoints[i - 1].gameObject.transform.position, raceCheckpoints[i].gameObject.transform.position);
        }
    }

    private void DrawPathPoints(Checkpoint[] raceCheckpoints)
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < raceCheckpoints.Length; i++)
        {
            Gizmos.DrawWireSphere(raceCheckpoints[i].gameObject.transform.position, 1);
        }
    }
}
