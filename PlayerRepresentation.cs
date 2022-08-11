using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Normal.Realtime;

public class PlayerRepresentation : MonoBehaviour
{
    private RealtimeView _realtimeView;
    private TrailRenderer _trailRenderer;

    private Color[] colors = {
        new Color(0.0f, 2.618f, 5.657f, 1.0f), // Blue
        new Color(1.853f, 29.354f, 0.66f, 1.0f), // Green
        new Color(22.627f, 5.657f, 0.0f, 1.0f), // Orange
        new Color(22.627f, 17.032f, 0.0f, 1.0f), // Yellow
    };

    private void Awake()
    {
        // Set physics timestep to 60hz
        Time.fixedDeltaTime = 1.0f / 60.0f;
        //Time.timeScale = 0.5f;

        _realtimeView = GetComponent<RealtimeView>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        // Call LocalStart() only if this instance is owned by the local client
        if (_realtimeView.isOwnedLocallyInHierarchy)
            LocalStart();

        var playerIndex = _realtimeView.ownerIDSelf;
        playerIndex = playerIndex % colors.Length;

        Debug.Log("Owner id:" + _realtimeView.ownerIDSelf + " Player index:" + playerIndex);
        // Debug.Log("Color:" + _trailRenderer.material.GetColor("_EmissionColor"));

        var color = colors[playerIndex];
        _trailRenderer.material.SetColor("_EmissionColor", color);
    }

    private void Update()
    {
        // Call LocalUpdate() only if this instance is owned by the local client
        if (_realtimeView.isOwnedLocallyInHierarchy)
            LocalUpdate();
    }

    private void FixedUpdate()
    {
        // Call LocalFixedUpdate() only if this instance is owned by the local client
        if (_realtimeView.isOwnedLocallyInHierarchy)
            LocalFixedUpdate();
    }

    private void LocalStart()
    {
        // Request ownership of the Player Representation
        GetComponent<RealtimeTransform>().RequestOwnership();

        // Disable collider locally, as that is located on the PlayerController
        GetComponent<SphereCollider>().enabled = false;
    }

    private void LocalUpdate()
    {
    }

    private void LocalFixedUpdate()
    {
    }
}
