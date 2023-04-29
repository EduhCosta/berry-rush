using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public int CheckpointOrder = 0;
    [SerializeField] public bool IsDriftStart = false;
    [SerializeField] public bool IsDriftEnd = false;

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerIdentifier.IsPlayer(other) || AIIdentifier.IsAI(other))
        {
            CartGameSettings cart = other.GetComponentInParent<CartGameSettings>();
            string id = cart.GetPlayerId();
            string name = cart.PlayerName;

            // Debug.Log($"{name} - {CheckpointOrder} - {transform.position}");

            RaceStorage.Instance.RegisterCheckpointByPlayer(id, name, CheckpointOrder);
        }

        if (AIIdentifier.IsAI(other))
        {
            if (IsDriftStart) AIIdentifier.GetAIKart(other).gameObject.GetComponent<AIInputController>().StartDrift();
            if (IsDriftEnd) AIIdentifier.GetAIKart(other).gameObject.GetComponent<AIInputController>().EndDrift();
        }
    }
}