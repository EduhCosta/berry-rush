using UnityEngine;

public class RaceCheckpoint
{
    public GameObject gameObject { get; set; }
    public int checkpointOrder { get; set; }
    public Vector3 forward { get; set; }


    public RaceCheckpoint(Checkpoint _gameCheckpoint)
    {
        GameObject _gameObject = _gameCheckpoint.gameObject;
        int _checkpointOrder = _gameCheckpoint.CheckpointOrder;
        Vector3 _forward = _gameObject.transform.TransformDirection(Vector3.forward);

        checkpointOrder = _checkpointOrder;
        gameObject = _gameObject;
        forward = _forward;
    }
}
