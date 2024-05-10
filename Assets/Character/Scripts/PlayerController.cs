using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f; // Movement Speed
    public float jumpForce = 1f; // Jumping Force
    public Animator animator; // Reference to the Animator component
    private Rigidbody rb; // Reference to the Rigidbody component

    void Start()
    {
        // Get reference to the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movement Input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Debug log to check input values
        Debug.Log("Horizontal Input: " + horizontalInput);
        Debug.Log("Vertical Input: " + verticalInput);

        // Adjust movement vector for separate horizontal and vertical movement
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed;

        // Update animator parameters based on input
        animator.SetFloat("Speed", movement.magnitude); // Use magnitude for movement speed
        animator.SetBool("IsWalkingLeft", horizontalInput < 0);
        animator.SetBool("IsWalkingRight", horizontalInput > 0);
        animator.SetBool("IsWalkingForward", verticalInput > 0);

        // Move the player
        transform.Translate(movement * Time.deltaTime);

        // Jumping Input
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }
    }

    void Jump()
    {
        // Trigger the jump animation
        animator.SetBool("JumpTrigger", true);

        // Apply vertical force for jumping
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        Debug.Log("Spacebar pressed!");

        // Reset the JumpTrigger after a delay
        Invoke("ResetJumpTrigger", 0.1f); // Adjust the delay as needed
    }

    void ResetJumpTrigger()
    {
        animator.SetBool("JumpTrigger", false);
    }

    bool IsGrounded()
    {
        // Use a raycast to check if the player is grounded
        return Physics.Raycast(transform.position, Vector3.down, 0.7f);
    }
}
