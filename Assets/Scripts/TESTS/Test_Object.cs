using Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using System;

public class Test_Object : MonoBehaviour, IInteractable
{
    public static event Action OnWeaponPickUp;
    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnWeaponPickUp?.Invoke();
            Destroy(gameObject);
        }

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
