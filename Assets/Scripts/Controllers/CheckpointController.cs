using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class CheckpointController : MonoBehaviour
{
    // This is there because is setting global configs and needs run after the Aweke, which is the singleton borns
    private void Start()
    {
        // Debug.Log(transform.childCount);
        // Set on storage the total number of checkpoints
        RaceStorage.Instance.SetTotalRacerCheckpoints(transform.childCount);

        // Set all race checkpoints os storage
        Checkpoint[] allCheckpoint = GetComponentsInChildren<Checkpoint>();
        Queue<RaceCheckpoint> raceCheckpoints = new();
        foreach (Checkpoint checkpoint in allCheckpoint)
        {
            // Debug.Log(checkpoint.transform.forward);
            raceCheckpoints.Enqueue(new RaceCheckpoint(checkpoint));
        }

        RaceStorage.Instance.SetRaceCheckpoints(raceCheckpoints);
    }

    private void FixedUpdate()
    {
        IdentifyWrongFlowByAllCheckpoints();
    }

    private void IdentifyWrongFlowByAllCheckpoints()
    {
        TrackFlowHandler[] flows = gameObject.GetComponentsInChildren<TrackFlowHandler>();
        bool tmpIsWrongFlow = false;

        foreach (TrackFlowHandler flow in flows)
        {
            tmpIsWrongFlow = tmpIsWrongFlow || flow.GetIsWrongFlow();
        }

        RaceStorage.Instance.SetIsWorngFlow(tmpIsWrongFlow);
    }
}
