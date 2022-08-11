using System;
using Cinemachine;
using UnityEngine;

public class PlayerInCave : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 1.0f)] float _highCutAmount = 1;
    CinemachineVirtualCamera _virtualCamera;
    CinemachineComponentBase _componentBase;

    private void Start()
    {
        _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _componentBase = _virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
    }

    private void OnTriggerEnter(Collider other)
    {
        SoundManager.Instance.SetParameter(SoundManager.AudioConstants.HighCut, _highCutAmount);
        if (_componentBase is CinemachineFramingTransposer)
        {
            (_componentBase as CinemachineFramingTransposer).m_CameraDistance = 8;
            // (_componentBase as CinemachineFramingTransposer).m_BiasY += 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SoundManager.Instance.SetParameter(SoundManager.AudioConstants.HighCut, 0);
        if (_componentBase is CinemachineFramingTransposer)
        {
            (_componentBase as CinemachineFramingTransposer).m_CameraDistance = 10;
            // (_componentBase as CinemachineFramingTransposer).m_BiasY -= 1;
        }
    }
}