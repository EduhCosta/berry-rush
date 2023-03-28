public class CheckpointData
{
  public string playerId { get; set; }
  public string playerName { get; set; }
  public int checkpointOrder { get; set; }
  public float timeStamp { get; set; }

  public CheckpointData(string _playerId, string _playerName, int _checkpointOrder, float _timeStamp)
  {
    playerId = _playerId;
    playerName = _playerName;
    checkpointOrder = _checkpointOrder;
    timeStamp = _timeStamp;
  }

    public override string ToString()
    {
        return $"(CheckpointData) Checkpoint {checkpointOrder} - {playerName}: {playerId}";
    }
}
