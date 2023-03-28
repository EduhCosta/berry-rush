using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackFlowHandler : MonoBehaviour
{
    [SerializeField] private bool IsWrongFlow = false;

    private void OnDrawGizmos()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.blue;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(transform.position, direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerIdentifier.IsPlayer(other))
        {
            IsWrongFlow = IsWrongForwardAngle(PlayerIdentifier.GetKart(other).transform);
        } 
        else if (AIIdentifier.IsAI(other))
        {
            IsWrongFlow = IsWrongForwardAngle(AIIdentifier.GetAIKart(other).transform);
        }
    }

    private bool IsWrongForwardAngle(Transform player)
    {
        Vector3 checkpointForward = transform.forward;
        Vector3 playerForward = player.forward;

        float angleBetweenForward = Vector3.Angle(playerForward, checkpointForward);

        // If this angle in bigger than 90 degrees we are runnig to wrong flow
        return angleBetweenForward > 90;
    }

    public bool GetIsWrongFlow()
    {
        return IsWrongFlow;
    }
}
