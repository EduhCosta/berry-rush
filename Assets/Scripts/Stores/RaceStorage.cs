using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Handle racer data
public class RaceStorage : MonoBehaviour
{
    public static RaceStorage Instance { get; private set; }

    private float _currentTime = 0f;
    private bool _isStartRacer = false;
    private int _totalCheckpoints = 0;
    private int _totalLaps = 0;
    private int _currentLap = 1;
    private Queue<LapData> QueueOfLaps = new();
    private Queue<CheckpointData> QueueOfCheckpoints = new();
    private Queue<RaceCheckpoint> QueueOfRaceCheckpoints = new();
    private bool _isWrongFlow = false;
    private LapHandler _lapHandler;
    private CartGameSettings[] _racers;
    private List<LapData> _currentLapByRacer = new();

    // Same as contructors in pure C#
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("More than one instance of a singleton");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (_isStartRacer)
        {
            UpdateTimer();
        }
    }

    /// <summary>
    /// Called when race starts, reponsible to set the initial state of game
    /// </summary>
    public void StartGame()
    {
        PodiumStore.Instance.EndGame(); // HERE
        _currentTime = 0f;
        _isStartRacer = true;
    }

    /// <summary>
    /// Update the general timer
    /// </summary>
    void UpdateTimer()
    {
        _currentTime += Time.deltaTime;
    }

    /// <summary>
    /// Called when race ends, reponsible to set the initial state of game
    /// </summary>
    public void EndGame()
    {
        _isStartRacer = false;
        _currentTime = 0f;
        _totalCheckpoints = 0;
        _totalLaps = 0;
        _currentLap = 1;
        QueueOfLaps = new();
        QueueOfCheckpoints = new();
        QueueOfRaceCheckpoints = new();
        _isWrongFlow = false;
        _lapHandler = null;
        _racers = null;
        _currentLapByRacer = new();
    }

    /// <summary>
    /// Set Player data on Storage
    /// <param name="id">Id of cart.</param>
    /// <param name="name">Name of cart.</param>
    /// <param name="checkpoint">Which checkpoit borns this calls.</param>
    /// </summary>
    public void RegisterCheckpointByPlayer(string id, string name, int checkpoint)
    {
        QueueOfCheckpoints.Enqueue(
            new CheckpointData(id, name, checkpoint, _currentTime)
        );
    }

    /// <summary>
    /// Define the total number of checkpoints
    /// </summary>
    public void SetTotalRacerCheckpoints(int totalCheckpoints)
    {
        _totalCheckpoints = totalCheckpoints;
    }

    /// <summary>
    /// Return the total number of checkpoints
    /// </summary>
    public int GetTotalCheckpoints()
    {
        return _totalCheckpoints;
    }

    /// <summary>
    /// Define the total number of laps
    /// </summary>
    public void SetTotalRaceLap(int totalLaps)
    {
        _totalLaps = totalLaps;
    }

    /// <summary>
    /// Return the total number of laps
    /// </summary>
    public int GetTotalRaceLap()
    {
        return _totalLaps;
    }

    /// <summary>
    /// Return a checkpoint queue based on racer Id
    /// </summary>
    public Queue<CheckpointData> GetCheckpointsByRacer(string racerId)
    {
        Queue<CheckpointData> racerQueue = new();

        foreach (CheckpointData register in QueueOfCheckpoints)
        {
            if (register.playerId.Equals(racerId))
            {
                racerQueue.Enqueue(register);
            }
        }

        return racerQueue;
    }

    /// <summary>
    /// Set end time of the last lap
    /// </summary>
    public void SetLastLapTimeStamp(string playerId, int lap)
    {
        QueueOfLaps.Enqueue(new LapData(playerId, lap, _currentTime));
    }

    /// <summary>
    /// Returns a queue of laps by racer id
    /// </summary>
    public Queue<LapData> GetLapsByRacerID(string racerId)
    {
        Queue<LapData> lapQueue = new();

        foreach (LapData register in QueueOfLaps)
        {
            if (register.playerId.Equals(racerId))
            {
                lapQueue.Enqueue(register);
            }
        }

        return lapQueue;
    }

    /// <summary>
    /// Returns last lap of queue
    /// </summary>
    public LapData GetLastLapByRacerID(string racerId)
    {
        if (GetLapsByRacerID(racerId).ToArray().Length > 0)
        {
            LapData[] arr = GetLapsByRacerID(racerId).ToArray();
            LapData lastElement = arr[arr.Length - 1];
            return lastElement;
        }

        return null;
    }

    /// <summary>
    /// Returns the current lap
    /// </summary>
    public int GetCurrentLap()
    {
        return _currentLap;
    }

    /// <summary>
    /// Define the current lap
    /// </summary>
    public void SetCurrentLap(int currentLap)
    {
        _currentLap = currentLap;
    }

    /// <summary>
    /// Returns the _isWrongFlow
    /// </summary>
    public bool GetIsWorngFlow()
    {
        return _isWrongFlow;
    }

    /// <summary>
    /// Define the _isWrongFlow
    /// </summary>
    public void SetIsWorngFlow(bool isWrongFlow)
    {
        _isWrongFlow = isWrongFlow;
    }

    /// <summary>
    /// Set all race checkpoints
    /// </summary>
    public void SetRaceCheckpoints(Queue<RaceCheckpoint> checkpoints)
    {
        QueueOfRaceCheckpoints = checkpoints;
    }

    /// <summary>
    /// Retrun all checkpoints of Race
    /// </summary>
    public Queue<RaceCheckpoint> GetRaceCheckpoints()
    {
        return QueueOfRaceCheckpoints;
    }

    /// <summary>
    /// Retrun a checkpoints by id
    /// </summary>
    public RaceCheckpoint GetRaceCheckpointsById(int id)
    {
        List<RaceCheckpoint> _arrayOfCheckpoints = new List<RaceCheckpoint>(QueueOfRaceCheckpoints.ToArray());
        return _arrayOfCheckpoints.Find(el => el.checkpointOrder == id);
    }

    /// <summary>
    /// Returns the track LapHandler
    /// </summary>
    public LapHandler GetLapHandler()
    {
        return _lapHandler;
    }

    /// <summary>
    /// Save the lapHandler on storage
    /// </summary>
    /// <param name="lapHandler">Game object which control the laps</param>
    public void SetLapHandler(LapHandler lapHandler)
    {
        _lapHandler = lapHandler;
    }

    /// <summary>
    /// Define the runners on race
    /// </summary>
    public void SetRacers(CartGameSettings[] racers)
    {
        _racers = racers;
    }

    /// <summary>
    /// Return the  runners on race
    /// </summary>
    public CartGameSettings[] GetRacers()
    {
        return _racers;
    }

    public void UpdateCurrentLapByRacer(string playerId)
    {
        if (_currentLapByRacer.Exists(r => r.playerId == playerId))
        {
            int index = _currentLapByRacer.FindIndex(e => e.playerId == playerId);
            int nextLap = _currentLapByRacer[index].lapDone + 1;
            _currentLapByRacer.Insert(index, new LapData(playerId, nextLap, _currentTime));
        }
        else
        {
            LapData element = new LapData(playerId, 1, _currentTime);
            _currentLapByRacer.Add(element);
        }
    }

    public LapData GetCurrentLapByRacer(string playerId)
    {
        if (!_currentLapByRacer.Exists(r => r.playerId == playerId))
        {
            UpdateCurrentLapByRacer(playerId);
            GetCurrentLapByRacer(playerId);
            return null;
        }

        return _currentLapByRacer.Find(e => e.playerId == playerId);
    }

    /// <summary>
    /// Returns the current clock of race
    /// </summary>
    public float GetCurrentTime()
    {
        return _currentTime;
    }

    /// <summary>
    /// Return if the current lap is the last one
    /// </summary>
    public bool IsLastLap()
    {
        return _currentLap == GetTotalRaceLap();
    }
}