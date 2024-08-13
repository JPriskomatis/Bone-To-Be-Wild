using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Interaction
{
    interface IInteractable
    {
        void Interact();
        void OnInteractEnter(); // Called when detection with the object starts
        void OnInteractExit();  // Called when detection with the object ends
    }

    public class Interactor : MonoBehaviour
    {
        [SerializeField] Transform InteractorSource;
        [SerializeField] float InteractRange;

        [SerializeField] private TextMeshProUGUI text;


        private IInteractable currentInteractable; // Track the currently detected interactable object

        private void Update()
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                GameObject detectedObject = hitInfo.collider.gameObject;

                if (detectedObject.TryGetComponent(out IInteractable interactObj))
                {
                    if (currentInteractable != interactObj)
                    {
                        // Exit the previous interactable object if exists
                        if (currentInteractable != null)
                            currentInteractable.OnInteractExit();

                        // Enter the new interactable object
                        interactObj.OnInteractEnter();
                        currentInteractable = interactObj;
                    }
                    interactObj.Interact();
                }
            }
            else
            {
                // No object detected, exit the previous interactable object if exists
                if (currentInteractable != null)
                {
                    currentInteractable.OnInteractExit();
                    currentInteractable = null;
                }

                if(text!= null)
                {
                    text.text = "";
                    text.gameObject.SetActive(false);

                }
            }
        }
    }
}