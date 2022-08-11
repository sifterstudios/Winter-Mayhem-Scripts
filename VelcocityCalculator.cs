using System;
using UnityEngine;

public class VelcocityCalculator : MonoBehaviour
{
    Rigidbody _rigidbody;
    [SerializeField] private float _clampAmount = 1500;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var currentMagnitude = _rigidbody.velocity.magnitude;
        if (currentMagnitude > _clampAmount)
        {
            currentMagnitude = _clampAmount;
        }

        var clampedVelocity = _rigidbody.velocity.magnitude / _clampAmount;
        SoundManager.Instance.SetParameter("PlayerSpeed", clampedVelocity);
    }
}