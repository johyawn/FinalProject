using System.Collections.Generic;
using UnityEngine;

namespace MyNamespace
{
    public class MyCharacterControl : MonoBehaviour
    {

        // serialized fields to put it in the Unity inspectorrr
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float turnSpeed = 200f;
        [SerializeField] private float jumpForce = 4f;

        [SerializeField] private Animator animator = null;
        [SerializeField] private Rigidbody rigidBody = null;

        [SerializeField] private ControlMode controlMode = ControlMode.Direct;

        // control modes

        private enum ControlMode
        {
            Tank,
            Direct
        }


        // movement control
        private float currentV = 0;
        private float currentH = 0;

        private readonly float interpolation = 10f;
        private readonly float walkScale = 0.33f;
        private readonly float backwardsWalkScale = 0.16f;
        private readonly float backwardRunScale = 0.66f;

        private bool wasGrounded;
        private Vector3 currentDirection = Vector3.zero;

        private float jumpTimeStamp = 0;
        private float minJumpInterval = 0.25f;
        private bool jumpInput = false;

        private bool isGrounded;
        private List<Collider> collisions = new List<Collider>();


        // when script is loaded...
        private void Awake()
        {
            if (!animator) animator = GetComponent<Animator>();
            if (!rigidBody) rigidBody = GetComponent<Rigidbody>();
        }

        // when another collider (or rigidbody) touches another rigidbody /collider

        private void OnCollisionEnter(Collision collision)
        {
            UpdateGroundedStatus(collision); // on ground
        }

        private void OnCollisionStay(Collision collision)
        {
            UpdateGroundedStatus(collision);
        }


        // when another collider (or rigidbody) stopss touching another rigidbody / collider.
        private void OnCollisionExit(Collision collision)
        {
            collisions.Remove(collision.collider);
            isGrounded = collisions.Count > 0;
        }

        // updates the grounded status, based on the above.

        private void UpdateGroundedStatus(Collision collision)
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                if (Vector3.Dot(contact.normal, Vector3.up) > 0.5f)
                {
                    if (!collisions.Contains(collision.collider))
                    {
                        collisions.Add(collision.collider);
                    }
                    isGrounded = true;
                    return;
                }
            }
            isGrounded = false;
        }

        private void Update()
        {
            // check jump imput
            if (!jumpInput && Input.GetKey(KeyCode.Space))
            {
                jumpInput = true;
            }
        }

        private void FixedUpdate()
        {
            animator.SetBool("Grounded", isGrounded);

            switch (controlMode)
            {
                case ControlMode.Direct:
                    DirectUpdate();
                    break;

                case ControlMode.Tank:
                    TankUpdate();
                    break;

                default:
                    Debug.LogError("Unsupported state");
                    break;
            }

            wasGrounded = isGrounded;
            jumpInput = false;
        }

        private void TankUpdate()
        {
            // getting input for vertical (forward + backwardss ) and horizontal (left + right) movement
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");


            // left shift key = walk!
            bool walk = Input.GetKey(KeyCode.LeftShift);

            if (v < 0)
            {
                v *= walk ? backwardsWalkScale : backwardRunScale;
            }
            else if (walk)
            {
                v *= walkScale;
            }

            currentV = Mathf.Lerp(currentV, v, Time.deltaTime * interpolation);
            currentH = Mathf.Lerp(currentH, h, Time.deltaTime * interpolation);

            transform.position += transform.forward * currentV * moveSpeed * Time.deltaTime;
            transform.Rotate(0, currentH * turnSpeed * Time.deltaTime, 0);

            animator.SetFloat("MoveSpeed", currentV);

            JumpingAndLanding();
        }

        private void DirectUpdate()
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            Transform camera = Camera.main.transform;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                v *= walkScale;
                h *= walkScale;
            }

            currentV = Mathf.Lerp(currentV, v, Time.deltaTime * interpolation);
            currentH = Mathf.Lerp(currentH, h, Time.deltaTime * interpolation);

            Vector3 direction = camera.forward * currentV + camera.right * currentH;
            direction = Vector3.ProjectOnPlane(direction, Vector3.up);

            if (direction != Vector3.zero)
            {
                currentDirection = Vector3.Slerp(currentDirection, direction, Time.deltaTime * interpolation);

                transform.rotation = Quaternion.LookRotation(currentDirection);
                transform.position += currentDirection * moveSpeed * Time.deltaTime;

                animator.SetFloat("MoveSpeed", currentDirection.magnitude);
            }

            JumpingAndLanding();
        }

        private void JumpingAndLanding()
        {
            bool jumpCooldownOver = (Time.time - jumpTimeStamp) >= minJumpInterval;

            if (jumpCooldownOver && isGrounded && jumpInput)
            {
                jumpTimeStamp = Time.time;
                rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
