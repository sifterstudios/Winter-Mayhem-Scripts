using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float DampTime = 0.2f;                 // Approximate time for the camera to refocus.
    public Transform Target;

    private Camera _camera;                        // Used for referencing the camera.
    private Vector3 _moveVelocity;                 // Reference velocity for the smooth damping of the position.
    private Vector3 _desiredPosition;              // The position the camera is moving towards.

    private void Awake()
    {
        _camera = GetComponentInChildren<Camera>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (Target == null)
            return;

        _desiredPosition = Target.position;

        // Smoothly transition to that position.
        transform.position = Vector3.SmoothDamp(transform.position, _desiredPosition, ref _moveVelocity, DampTime);
    }
}