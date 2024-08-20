using Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tavern_Door: Base_Door
{
    public static event Action OnEntrance;
    public static event Action OnExit;


    public override void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E) && !locked)
        {
            if (!isOpen)
            {
                //Open Door
                ToggleDoor();
                OnEntrance?.Invoke();                
            }
            else
            {
                ToggleDoor();
                OnExit?.Invoke();
            }
        }
    }
}
