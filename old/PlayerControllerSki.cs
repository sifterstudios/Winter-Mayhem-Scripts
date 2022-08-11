using UnityEngine;
using Normal.Realtime;
using UnityEngine.InputSystem;

public class PlayerControllerSki : MonoBehaviour
{
    public Transform PlayerRepresentation;

    [SerializeField] private float _rotationSpeed = 200.0f;

    private Rigidbody _rigidbody;
    private SphereCollider _sphereCollider;

    private float _angle = 90.0f;
    private Vector2 _moveVal;

    private void Awake()
    {
        // Set physics timestep to 60hz
        Time.fixedDeltaTime = 1.0f / 60.0f;
        //Time.timeScale = 0.5f;

        _rigidbody = GetComponent<Rigidbody>();
        _sphereCollider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (PlayerRepresentation == null)
        {
            return;
        }

        float velocityAdjustment = 1.0f;
        Vector3 normal;
        if (isGrounded())
        {
            normal = getSurfaceNormal();
            //normal = Vector3.up;

            // When grounded it is harder to turn, make it depend on velocity
            var velocityMagnitude = _rigidbody.velocity.magnitude;
            if (velocityMagnitude > 30.0f)
            {
                velocityMagnitude = 30.0f;
            }

            if (velocityMagnitude <= 0.0f)
            {
                velocityMagnitude = 1.0f;
            }

            velocityAdjustment = velocityMagnitude / 30.0f;
        }
        else
        {
            normal = Vector3.up;
        }

        _angle += _moveVal.x * _rotationSpeed * Time.deltaTime * velocityAdjustment;

        Quaternion rotation = Quaternion.Euler(0, _angle, 0);

        // transform.RotateAround(transform.position, transform.up, transform.localEulerAngles.y);
        // transform.Rotate(0.0f, -moveVal.x * RotationSpeed * Time.deltaTime, 0.0f);

        Quaternion targetAlignment = Quaternion.FromToRotation(Vector3.up, normal) * rotation;

        // Update representation with position and rotation
        PlayerRepresentation.position = transform.position;
        PlayerRepresentation.rotation =
            Quaternion.Lerp(PlayerRepresentation.rotation, targetAlignment, Time.deltaTime * 5.0f);
    }

    private void FixedUpdate()
    {
        if (PlayerRepresentation == null)
        {
            return;
        }

        if (isGrounded())
        {
            var velocityMagnitude = _rigidbody.velocity.magnitude;
            Vector3 newDir = PlayerRepresentation.TransformDirection(Vector3.forward) * velocityMagnitude;
            _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, newDir, Time.deltaTime * 2.0f);
            // SoundManager.Land();
        }
        else
        {
            // SoundManager.Jump();
        }
    }

    void OnDrawGizmos()
    {
        if (PlayerRepresentation == null)
        {
            return;
        }

        var r = GetComponent<Rigidbody>();
        var velocityMagnitude = r.velocity.magnitude;
        Vector3 newDir = PlayerRepresentation.TransformDirection(Vector3.forward) * velocityMagnitude;
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, newDir);

        if (Application.isPlaying)
        {
            if (isGrounded())
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(transform.position, _sphereCollider.radius);
            }
        }
    }

    void OnMove(InputValue value)
    {
        _moveVal = value.Get<Vector2>();
    }

    void OnFire()
    {
        Debug.Log("fire!");

        if (PlayerRepresentation == null)
        {
            return;
        }

        /*        Animator animator = PlayerRepresentation.GetComponentInChildren<Animator>();
                if (animator == null)
                {
                    Debug.Log("animator not found");
                }

                bool falling = animator.GetBool("Falling");
                animator.SetBool("Falling", !falling);
        */
    }

    bool isGrounded()
    {
        RaycastHit hitInfo;
        return Physics.SphereCast(transform.position, _sphereCollider.radius, Vector3.down, out hitInfo,
            _sphereCollider.radius);
    }

    Vector3 getSurfaceNormal()
    {
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position, _sphereCollider.radius, Vector3.down, out hitInfo))
        {
            return hitInfo.normal;
        }

        /*        if (Physics.Raycast(transform.position, -transform.up, out hitInfo))
                {
                    return hitInfo.normal;
                }
          */
        return Vector3.up;
    }
}