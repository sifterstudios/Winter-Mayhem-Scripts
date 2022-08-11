using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerSnowball : MonoBehaviour
{
    [Tooltip("Max angle you can turn")] [SerializeField]
    private float _rotationMaxAngle = 45.0f;

    [Tooltip("How fast to turn")] [SerializeField]
    private float _rotationSpeed = 2.0f;

    [Tooltip("How much to jump")] [SerializeField]
    private float _jumpForce = 20000.0f;

    [Tooltip("Velocity needs to be below this number to boost your speed")] [SerializeField]
    private float _forwardVelocityLimit = 3.0f;

    [Tooltip("How much to boost forward")] [SerializeField]
    private float _forwardForce = 5000.0f;

    private Transform _playerRepresentation;
    private Rigidbody _rigidbody;
    private SphereCollider _sphereCollider;

    private Vector2 _inputMovement;
    private Vector3 _targetDirection;
    private bool _jump = false;

    public void SetPlayerRepresentation(Transform player)
    {
        _playerRepresentation = player;
    }

    private void Awake()
    {
        // Set physics timestep to 60hz
        Time.fixedDeltaTime = 1.0f / 60.0f;
        //Time.timeScale = 0.2f;

        _rigidbody = GetComponent<Rigidbody>();
        _sphereCollider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (_playerRepresentation == null)
            return;

        // Update representation with position and rotation
        _playerRepresentation.position = transform.position;
        _playerRepresentation.rotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        PhysicMaterial physicMaterial;
        var grounded = isGrounded(out physicMaterial);
        float groundFriction = 1.0f;
        if (physicMaterial)
        {
            groundFriction = physicMaterial.dynamicFriction;
        }

        // Jump
        if (grounded && _jump)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Force);
            _jump = false;
            // Play Jump Sound
            // SoundManager.Instance.Jump(gameObject.transform);
        }

        // Forward movement
        if (grounded && _inputMovement.y > 0.0f)
        {
            if (_rigidbody.velocity.magnitude < _forwardVelocityLimit)
            {
                _rigidbody.AddForce(_rigidbody.velocity * _forwardForce, ForceMode.Force);
            }
        }

        // Modify sidewise direction
        if (grounded)
        {
            _targetDirection = Quaternion.AngleAxis(_inputMovement.x * _rotationMaxAngle, Vector3.up) *
                               _rigidbody.velocity;
            _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, _targetDirection,
                Time.deltaTime * _rotationSpeed * groundFriction);
        }
        else
        {
            _targetDirection = _rigidbody.velocity;
        }

        // Sounds
        if (grounded)
        {
            // TODO: Implement 'HasLanded' or something to avoid this calling every frame
            // SoundManager.Instance.Land(_rigidbody.velocity.magnitude, gameObject.transform, true);
        }
    }

    void OnMove(InputValue value)
    {
        _inputMovement = value.Get<Vector2>();
    }

    void OnFire()
    {
        _jump = true;
    }

    bool isGrounded(out PhysicMaterial physicMaterial)
    {
        RaycastHit hitInfo;
        var hit = Physics.Raycast(transform.position, Vector3.down, out hitInfo);
        //        var hit = Physics.SphereCast(_sphereCollider.transform.position, _sphereCollider.radius, Vector3.down, out hitInfo);

        var isGrounded = hitInfo.distance <= _sphereCollider.radius + 0.15f;
        ;
        if (isGrounded && hitInfo.collider)
        {
            physicMaterial = hitInfo.collider.sharedMaterial;
        }
        else
        {
            physicMaterial = null;
        }

        return isGrounded;
    }

    ///////////////////////////////////////////////////////////////////////////////

    void OnDrawGizmos()
    {
        gizmoDrawVelocityDirection(Color.blue);
        gizmoDrawTargetDirection(Color.green);
        //gizmoDrawGrounded(Color.red);
    }

    private void gizmoDrawTargetDirection(Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawRay(transform.position, _targetDirection);
    }

    private void gizmoDrawGrounded(Color color)
    {
        if (Application.isPlaying)
        {
            if (isGrounded(out _))
            {
                Gizmos.color = color;
                Gizmos.DrawSphere(transform.position, _sphereCollider.radius);
            }
        }
    }

    private void gizmoDrawVelocityDirection(Color color)
    {
        var r = GetComponent<Rigidbody>();
        Gizmos.color = color;
        Vector3 velocityDirection = r.velocity;
        Gizmos.DrawRay(transform.position, velocityDirection);
    }
}