using Interaction;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public float rotationSpeed;
    private bool isOpen = false;

    private Quaternion openRotation;
    private Quaternion closedRotation;

    [SerializeField] private bool locked;

    private void Start()
    {
        openRotation = Quaternion.Euler(0f, 90f, 0f);
        closedRotation = Quaternion.Euler(0f, 0f, 0f);

    }
    public void Interact()
    {
        if(Input.GetKeyDown(KeyCode.E) && !locked)
        {
            //Open Door
            ToggleDoor();
        }
    }

    public void OnInteractEnter()
    {
        TextAppear.SetText("Open");
    }

    public void OnInteractExit()
    {
        TextAppear.RemoveText();
    }

    public void ToggleDoor()
    {
        if (isOpen)
        {
            StartCoroutine(RotateDoor(transform.rotation * Quaternion.Euler(0f, -90f, 0f)));

        }
        else
        {
            StartCoroutine(RotateDoor(transform.rotation * Quaternion.Euler(0f, 90f, 0f)));

        }

        isOpen = !isOpen; // Toggle the door state
    }

    private IEnumerator RotateDoor(Quaternion targetRotation)
    {
        Quaternion startRotation = transform.rotation;
        float timeElapsed = 0f;

        // Rotate the door smoothly to the target rotation
        while (timeElapsed < 1f)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed);
            timeElapsed += Time.deltaTime * rotationSpeed;
            yield return null;
        }

        transform.rotation = targetRotation; // Ensure the final rotation is set
    }


}
