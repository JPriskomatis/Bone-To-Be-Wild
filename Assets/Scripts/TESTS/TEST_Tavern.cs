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

    public override void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E) && !locked)
        {
            //Open Door
            ToggleDoor();
            StartAudio();
        }


    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            AudioManager.instance.PlaySFX("Hey There");
        }
    }
}
