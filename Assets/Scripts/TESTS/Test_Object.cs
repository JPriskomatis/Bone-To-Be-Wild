using Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Object : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if(Input.GetKeyDown(KeyCode.E))
            Debug.Log("Just interacted");
    }

    public void OnInteractEnter()
    {
        Debug.Log("Can interact");
    }

    public void OnInteractExit()
    {
        Debug.Log("End of interaction");
    }
}
