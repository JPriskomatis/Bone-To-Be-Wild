using Interaction;
using PlayerSpace;
using System.Collections;
using UI;
using UnityEngine;

namespace Buildings
{
    public class Note_BountyBoard : MonoBehaviour, IInteractable
    {
        public Camera mainCamera;  // Reference to the main camera
        public Transform notePosition;  // Position in front of the note
        public float zoomSpeed = 2f;  // Speed of zooming in/out

        // Variables to store the original camera settings
        private Vector3 originalCameraLocalPosition;
        private Quaternion originalCameraLocalRotation;

        private bool isZoomedIn = false;  // Flag to check if camera is zoomed in

        private Vector3 originalPosition;


        public float distance = 5.0f; // Distance from the GameObject
        public Vector3 offset = Vector3.zero; // Optional offset to adjust the final position

        void Start()
        {
            // Initialize the original position
            originalPosition = mainCamera.transform.position;
        }


        public void Interact()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Vector3 targetPosition = transform.position + transform.forward * distance + offset;
                StartCoroutine(MoveCamera(targetPosition, 0.5f));
                
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                Vector3 originalLocalPosition = new Vector3(0f, 0f, 0f);
                float originalFOV = 60f;
                float zoomDuration = 0.5f;

                StartCoroutine(MoveCameraBack(originalLocalPosition, originalFOV, zoomDuration));
                TextAppear.RemoveText();
            } 
        }

        private IEnumerator MoveCamera(Vector3 targetPosition, float duration)
        {
            // Lock player movement
            PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
            playerMovement.enabled = false;

            float elapsedTime = 0f;
            Vector3 initialPosition = Camera.main.transform.position;
            Quaternion initialRotation = Camera.main.transform.rotation;

            // Adjust target position to maintain the current z position
            Vector3 targetPositionWithCurrentZ = new Vector3(targetPosition.x, targetPosition.y + 0.7f, targetPosition.z - 2f);

            // Calculate the final rotation needed to look at the target
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - targetPositionWithCurrentZ);

            while (elapsedTime < duration)
            {
                float t = Mathf.Clamp01(elapsedTime / duration);

                // Smoothly interpolate the position
                Camera.main.transform.position = Vector3.Lerp(initialPosition, targetPositionWithCurrentZ, t);

                // Smoothly interpolate the rotation
                Camera.main.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure the final position and rotation are exact
            Camera.main.transform.position = targetPositionWithCurrentZ;
            Camera.main.transform.LookAt(targetPosition);
        }

        private IEnumerator MoveCameraBack(Vector3 originalLocalPosition, float originalFOV, float duration)
        {
            float elapsedTime = 0f;

            // Get the current world position, rotation, and FOV of the camera
            Vector3 currentWorldPosition = Camera.main.transform.position;
            Quaternion currentWorldRotation = Camera.main.transform.rotation;
            float currentFOV = Camera.main.fieldOfView;

            // Convert the original local position to world position
            Vector3 originalWorldPosition = Camera.main.transform.parent.TransformPoint(originalLocalPosition);

            // Calculate the original rotation in world space
            Quaternion originalWorldRotation = Camera.main.transform.parent.rotation;

            while (elapsedTime < duration)
            {
                float t = Mathf.Clamp01(elapsedTime / duration);

                // Smoothly interpolate position, rotation, and FOV
                Camera.main.transform.position = Vector3.Lerp(currentWorldPosition, originalWorldPosition, t);
                Camera.main.transform.rotation = Quaternion.Slerp(currentWorldRotation, originalWorldRotation, t);
                Camera.main.fieldOfView = Mathf.Lerp(currentFOV, originalFOV, t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure the final values are exact
            Camera.main.transform.position = originalWorldPosition;
            Camera.main.transform.rotation = originalWorldRotation;
            Camera.main.fieldOfView = originalFOV;

            // Unlock player movement
            PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
            playerMovement.enabled = true;
        }
        public void OnInteractEnter()
        {
            if (!isZoomedIn)
            {
                TextAppear.SetText("Read Note");
            }
        }

        public void OnInteractExit()
        {
            TextAppear.RemoveText();
            // No camera reset here
        }

        

        
    }
}
