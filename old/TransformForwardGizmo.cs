using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformForwardGizmo : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(transform.position, direction);
    }
}
