using System;
using Cinemachine;
using UnityEngine;

public class SwitchCameraAngleInsideTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera MainAngle;
    [SerializeField] private CinemachineVirtualCamera SwitchAngle;

    private void OnTriggerEnter(Collider other)
    {
        MainAngle.Priority = 5;
        SwitchAngle.Priority = 10;
    }

    private void OnTriggerExit(Collider other)
    {
        MainAngle.Priority = 10;
        SwitchAngle.Priority = 5;
    }
}