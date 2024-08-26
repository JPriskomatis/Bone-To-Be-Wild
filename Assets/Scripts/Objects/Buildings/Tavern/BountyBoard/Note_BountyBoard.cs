using Interaction;
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

        void Start()
        {
            // Initialize the original position
            originalPosition = mainCamera.transform.position;
        }


        public void Interact()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(MoveCamera(this.transform.position, 0.5f));
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
            float elapsedTime = 0f;
            Vector3 initialPosition = Camera.main.transform.position;

            // Ensure the target position maintains the current z position
            Vector3 targetPositionWithCurrentZ = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z - 2.5f);

            while (elapsedTime < duration)
            {
                float t = Mathf.Clamp01(elapsedTime / duration);

                // Smoothly interpolate the position
                Camera.main.transform.position = Vector3.Lerp(initialPosition, targetPositionWithCurrentZ, t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure the final position is exact
            Camera.main.transform.position = targetPositionWithCurrentZ;
        }

        private IEnumerator MoveCameraBack(Vector3 originalLocalPosition, float originalFOV, float duration)
        {
            float elapsedTime = 0f;

            // Get the current world position and rotation of the camera
            Vector3 currentWorldPosition = Camera.main.transform.position;
            Quaternion currentWorldRotation = Camera.main.transform.rotation;
            float currentFOV = Camera.main.fieldOfView;

            // Convert the original local position to world position
            Vector3 originalWorldPosition = Camera.main.transform.parent.TransformPoint(originalLocalPosition);

            while (elapsedTime < duration)
            {
                float t = Mathf.Clamp01(elapsedTime / duration);

                // Smoothly interpolate position, rotation, and FOV
                Vector3 newWorldPosition = Vector3.Lerp(currentWorldPosition, originalWorldPosition, t);
                Camera.main.transform.position = newWorldPosition;
                Camera.main.fieldOfView = Mathf.Lerp(currentFOV, originalFOV, t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure the final values are exact
            Camera.main.transform.position = originalWorldPosition;
            Camera.main.fieldOfView = originalFOV;
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
