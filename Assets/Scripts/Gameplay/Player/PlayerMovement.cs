using Dialoguespace;
using gameStateSpace;
using questSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSpace
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float moveSpeed = 5f;        // Speed of movement
        private float originalSpeed;
        public float lookSpeed = 2f;        // Speed of mouse look
        public float upDownRange = 60f;     // Vertical camera rotation range
        public float gravity = -9.81f;      // Gravity force
        public float terminalVelocity = -20f; // Maximum falling speed to avoid going too fast

        private CharacterController controller;
        private Camera playerCamera;
        private float rotationX = 0f;       // Current vertical rotation of the camera

        [SerializeField] private Animator anim;

        private Vector3 velocity;           // Current velocity of the player
        private bool isWalking;

        private bool isPaused;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
            playerCamera = GetComponentInChildren<Camera>();

            // Lock cursor to center and make it invisible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            originalSpeed = moveSpeed;

        }
        private void OnEnable()
        {
            GameStatController.OnPause += PauseCamera;
        }

        private void OnDisable()
        {
            GameStatController.OnPause -= PauseCamera;
        }

        public void PauseCamera(bool pause)
        {
            isPaused = pause;
        }

        private void Update()
        {
            if (!DialogueManager.GetInstance().dialogueIsPlaying)
            {
                HandleMovement();
                
                if(!isPaused)
                    HandleMouseLook();
            }
            else
            {
                anim.SetBool("walk", false);
            }
            
        }

        private void HandleMovement()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = originalSpeed * 1.5f;
            }
            else
            {
                moveSpeed = originalSpeed;
            }
            // Retrieve input for movement
            float moveDirectionY = Input.GetAxis("Vertical") * moveSpeed;
            float moveDirectionX = Input.GetAxis("Horizontal") * moveSpeed;

            Vector3 move = transform.right * moveDirectionX + transform.forward * moveDirectionY;

            // Apply movement
            controller.Move(move * Time.deltaTime);

            // Apply gravity
            if (velocity.y > terminalVelocity)
            {
                velocity.y += gravity * Time.deltaTime;
            }

            // Apply gravity to the movement
            controller.Move(velocity * Time.deltaTime);

            // Update walking animation
            isWalking = move.magnitude > 0.1f; // Adjust the threshold as needed
            anim.SetBool("walk", isWalking);
        }

        private void HandleMouseLook()
        {
            
            // Retrieve input for mouse look
            float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

            // Rotate player horizontally
            transform.Rotate(Vector3.up * mouseX);

            // Rotate camera vertically
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -upDownRange, upDownRange);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        }
    }
}
