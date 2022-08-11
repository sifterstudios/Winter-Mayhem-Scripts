using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborne : MonoBehaviour
{
    [SerializeField] private float _clampValue = 30.0f;

    void Update()
    {
        float distanceOverGround = calculateDistanceOverGround();

        if (distanceOverGround < 0.0f)
        {
            distanceOverGround = 0.0f;
        }
        if (distanceOverGround > _clampValue)
        {
            distanceOverGround = _clampValue;
        }

        float normalizedDistanceOverGround = distanceOverGround / _clampValue;
        //Debug.Log("Distance over ground: " + normalizedDistanceOverGround);

        SoundManager.Instance.SetParameter(SoundManager.AudioConstants.AirborneHeight, normalizedDistanceOverGround);
    }

    float calculateDistanceOverGround()
    {
        RaycastHit hitInfo;
        var hit = Physics.Raycast(transform.position, Vector3.down, out hitInfo);
        return hitInfo.distance;
    }
}
