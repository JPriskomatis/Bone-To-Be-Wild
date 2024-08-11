using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSpace
{
    public class PlayerMovement : MonoBehaviour
    {

        [Header("Movement Settings")]
        public float moveSpeed = 5f;        // Speed of movement
        public float lookSpeed = 2f;        // Speed of mouse look
        public float upDownRange = 60f;     // Vertical camera rotation range


        private CharacterController controller;
        private Camera playerCamera;
        private float rotationX = 0f;       // Current vertical rotation of the camera

        [SerializeField] private Animator anim;

        bool isWalking;
        private float lastMouseX;


        public GameObject vfxPrefab; // Drag your VFX prefab here in the Inspector
        public Vector3 offset = new Vector3(0, 0, 1);


        private void Start()
        {
            controller = GetComponent<CharacterController>();
            playerCamera = GetComponentInChildren<Camera>();

            // Lock cursor to center and make it invisible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            HandleMovement();
            HandleMouseLook();
            //if(Input.GetMouseButtonDown(0))
            //{
            //    anim.SetTrigger("attack");
            //}
        }

        public void CastSpell()
        {
            
            Vector3 spawnPosition = transform.position + transform.TransformDirection(offset);
            GameObject fvxInstance = Instantiate(vfxPrefab, spawnPosition, Quaternion.identity);
            Destroy(fvxInstance, 1f);
        }

        private void HandleMovement()
        {
            // Retrieve input for movement
            float moveDirectionY = Input.GetAxis("Vertical") * moveSpeed;
            float moveDirectionX = Input.GetAxis("Horizontal") * moveSpeed;

            Vector3 move = transform.right * moveDirectionX + transform.forward * moveDirectionY;
            controller.Move(move * Time.deltaTime);

            bool isWalking = move.magnitude > 0.1f; // Adjust the threshold as needed
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

            lastMouseX = mouseX;

            CheckTurnRightAnimation();

        }

        private void CheckTurnRightAnimation()
        {
            if (lastMouseX > 0.1f) // Adjust threshold as needed
            {
                anim.SetTrigger("turnRight");
            }
        }
    }

}
