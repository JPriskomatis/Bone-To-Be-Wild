using QuickOutline;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
namespace Interaction
{
    public interface IInteractable
    {
        void Interact();
        void OnInteractEnter(); //Called when detection with the object starts;
        void OnInteractExit();  //Called when detection with the object ends;
    }
    
    public class Interactor : MonoBehaviour
    {


        
        [SerializeField] Transform InteractorSource;
        [SerializeField] float InteractRange;

        [SerializeField] private TextMeshProUGUI text;

        public GameObject detectedObject;


        private IInteractable currentInteractable; // Track the currently detected interactable object

        //We initialize the TextAppear in order to grab the textmeshpro text;
        private void Start()
        {
            TextAppear.Initialize();
        }
        private void Update()
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                detectedObject = hitInfo.collider.gameObject;

                if (detectedObject.TryGetComponent(out IInteractable interactObj))
                {
                    if (currentInteractable != interactObj)
                    {
                        //Exit the previous interactable object if exists;
                        if (currentInteractable != null)
                            currentInteractable.OnInteractExit();

                        //Enter the new interactable object;
                        interactObj.OnInteractEnter();

                        //If its an item;
                        if (detectedObject.CompareTag("Item"))
                        {
                            detectedObject.GetComponent<Outline>().enabled = true;
                        }
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
                if (detectedObject.CompareTag("Item"))
                {
                    detectedObject.GetComponent<Outline>().enabled = false;
                }
                text.text = "";
                text.gameObject.SetActive(false);
            }
        }
    }
}