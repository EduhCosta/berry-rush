public class LapData
{
  public string playerId { get; set; }
  public int lapDone { get; set; }
  public float timeStamp { get; set; }

  public LapData(string _playerId, int _lapDone, float _timeStamp)
  {
    playerId = _playerId;
    lapDone = _lapDone;
    timeStamp = _timeStamp;
  }
}
