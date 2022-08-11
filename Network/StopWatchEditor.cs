using System;
using UnityEngine;
using UnityEditor;
/*
[CustomEditor(typeof(Stopwatch))]
public class StopwatchEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Stopwatch stopwatch = (Stopwatch)target;

        // Only enable in play mode
        EditorGUI.BeginDisabledGroup(Application.isPlaying == false);

        // Show the time
        TimeSpan timeSpan = TimeSpan.FromSeconds(stopwatch.time);
        EditorGUILayout.LabelField($"Time: {timeSpan:mm\\:ss\\.ff}");

        // Show a button to start the timer
        //        if (GUILayout.Button("Start"))
        //            stopwatch.StartStopwatch();

        EditorGUI.EndDisabledGroup();

        // Refresh the inspector while in play mode
        if (Application.isPlaying) EditorUtility.SetDirty(target);
    }
}
*/