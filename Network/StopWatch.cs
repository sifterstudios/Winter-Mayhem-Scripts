using Normal.Realtime;
using UnityEngine;
using System;

/*
public class Stopwatch : Monobehaviour, RealtimeComponent<StopwatchModel>
{
    //   private float _countdown = 5.0f;

    //private DateTimeOffset _countdown = DateTimeOffset.FromUnixTimeSeconds(epochSeconds);
    public float time
    {
        get
        {
            // Return 0 if we're not connected to the room yet.
            if (model == null) return 0;

            // Make sure the stopwatch is running
            //Debug.Log("no model starttime yet");
            if (model.startTime == 0.0) return -1;

            // Calculate how much time has passed
            Debug.Log("have time");
            var timePassed = realtime.room.time - model.startTime;
            return (float)timePassed;

            //var remainingTime = _countdown - timePassed;

            //if (remainingTime < 0.0)
            //                remainingTime = 0.0;

            //return (float)remainingTime;
            //            return (float)(realtime.room.time - model.startTime);
        }
    }

    protected override void OnRealtimeModelReplaced(StopwatchModel previousModel, StopwatchModel currentModel)
    {
        if (previousModel != null)
        {
            previousModel.startTimeDidChange -= ActiveStateChanged;
        }
        if (currentModel != null)
        {
            currentModel.startTimeDidChange += ActiveStateChanged;
        }
    }

    void ActiveStateChanged(StopwatchModel model, double time)
    {
        Debug.Log("ActiveStateChanged: " + time);
        StartStopwatch();
    }

    private void Awake()
    {
        //        StartStopwatch();
    }

    public void StartStopwatch()
    {
        if (realtime == null)
        {
            Debug.Log("realtime is null");
            return;
        }
        if (realtime.room == null)
        {
            Debug.Log("realtime.room is null");
            return;
        }
        if (model == null)
        {
            Debug.Log("model is null");
            return;
        }

        Debug.Log("starting countdown");
        model.startTime = realtime.room.time;
    }
}
*/