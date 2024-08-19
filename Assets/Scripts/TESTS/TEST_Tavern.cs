using Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_Tavern : Door
{
    public void StartAudio()
    {
        AudioManager.instance.PlayMusic("Tavern Background");

    }
    public void StopAudio()
    {
        AudioManager.instance.StopMusic("Tavern Background");

    }

    public override void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E) && !locked)
        {
            if (!isOpen)
            {
                //Open Door
                ToggleDoor();
                StartAudio();
            }
            else
            {
                ToggleDoor();
                StopAudio();
            }

            
        }


    }


}
