using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;

    private float raceTimer;
    private float raceProgress;
    private DateTime raceStart;
    private DateTime raceEnd;

    private TimeSpan bestTime = TimeSpan.MaxValue;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        raceEnd = DateTime.MaxValue;
    }

    public void RestartGame()
    {
        raceStart = DateTime.Now;
        raceEnd = DateTime.MinValue;
        raceTimer = 0.0f;
        raceProgress = 0.0f;
    }

    public void ReachGoal()
    {
        raceEnd = DateTime.Now;
        raceProgress = 1.0f;

        var time = GetTimer();
        if (time < bestTime)
        {
            bestTime = time;
        }
    }

    public float GetRaceProgress()
    {
        return raceProgress;
    }

    public TimeSpan GetTimer()
    {
        // Not started yet
        if (raceEnd == DateTime.MaxValue)
        {
            return TimeSpan.Zero;
        }

        // Reached goal
        if (raceEnd == DateTime.MinValue)
        {
            DateTime now = DateTime.Now;
            return now - raceStart;
        }

        // In progress
        return raceEnd - raceStart;
    }

    public TimeSpan GetBestTime()
    {
        return bestTime;
    }

    public void UpdateProgress(float raceProgress)
    {
        if (this.raceProgress == 1.0) // We already reached goal
        {
            return;
        }
        this.raceProgress = raceProgress;
    }
}
