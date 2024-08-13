using Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

public class Test_Object : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if(Input.GetKeyDown(KeyCode.E))
            TextAppear.SetText("Interacted");
    }

    public void OnInteractEnter()
    {
        TextAppear.SetText("Interact");
    }

    public void OnInteractExit()
    {
        TextAppear.SetText("End of Interaction");
    }
}
