using UnityEngine;
using UnityEngine.InputSystem; 

/// <summary>
/// Moves forward/backward and rotates with WASD/Arrow keys.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Tooltip("Forward/back speed (units/sec).")]
    public float speed = 5.0f;

    [Tooltip("Turn speed (degrees/sec).")]
    public float rotationSpeed = 120.0f;

    [Tooltip("Jumping Force")]
    public float jumpForce = 5.0f;

    private Rigidbody rb; 
    //variable to hold reference to Ground Check Script for Jump Functionality
    private GroundCheck groundCheckScript;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null) Debug.LogWarning("PlayerController needs a Rigidbody.");

        // Get the reference to GroundCheck Script on the same GameObject
        groundCheckScript = GetComponent<GroundCheck>();
        if (groundCheckScript == null) Debug.LogWarning("PlayerController is missing Ground Check Script.");
    }

    private void Update()
    {

    }

    private void FixedUpdate() 
    {
        Vector2 moveInput = Vector2.zero;

        // Forward/backward
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)   moveInput.y = 1f;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) moveInput.y = -1f;

        // Left/right (rotation)
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) moveInput.x = -1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)moveInput.x = 1f;

        // Move in facing direction 
        Vector3 movement = transform.forward * moveInput.y * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Y-axis rotation (invert when going backwards)
        float turnDirection = moveInput.x;
        if (moveInput.y < 0)
            turnDirection = -turnDirection;

        float turn = turnDirection * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);

        //Jump Button Functionality
        if (Keyboard.current.spaceKey.isPressed && groundCheckScript != null)
        {
            if(groundCheckScript.CheckIfGrounded()){
                rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            }      
        }
    }
}
