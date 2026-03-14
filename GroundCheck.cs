using System;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [Tooltip("An Object created and Placed at the Feet of the Character, will be used for the ground check")]
    public Transform groundCheckPoint;
    [Tooltip("Tag will be Considered the Ground Layer or grounded Objects")]
    public LayerMask groundLayer;
    [Tooltip("Distance from the ground to check")]
    public float groundCheckDistance = 0.05f;
    public float groundCheckRadius = 0.1f;

    private bool isGrounded;

    public Boolean CheckIfGrounded()
    {
        isGrounded = Physics.SphereCast(
            groundCheckPoint.position,
            groundCheckRadius,
            Vector3.down,
            out RaycastHit hitInfo,
            groundCheckDistance,
            groundLayer,
            QueryTriggerInteraction.Ignore
        );

        // This tells you IF you hit something and WHAT you hit
        if (isGrounded) {
            Debug.Log("Grounded on: " + hitInfo.collider.name);
        } else {
            Debug.Log("Not Grounded");
        }
        return isGrounded;
    }

    // Visualizes the sphere at the groundCheck position
        void OnDrawGizmosSelected()
        {
            if (groundCheckPoint == null) return;
            Gizmos.color = Color.yellow;
            Vector3 endPoint = groundCheckPoint.position + (Vector3.down * groundCheckDistance);
            Gizmos.DrawWireSphere(endPoint, groundCheckRadius);
        }
}
