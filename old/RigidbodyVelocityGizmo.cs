using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyVelocityGizmo : MonoBehaviour
{
    void OnDrawGizmos()
    {
        var r = GetComponent<Rigidbody>();
        Gizmos.color = Color.blue;
        Vector3 velocityDirection = r.velocity;
        //        Gizmos.DrawRay(transform.position, velocityDirection);
    }
}
