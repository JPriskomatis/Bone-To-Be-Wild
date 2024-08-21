using Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings
{

    public class Tavern_Door : Base_Door
    {
        public static event Action OnEntrance;

        [SerializeField]
        GameObject stopMusic;
        private void OnEnable()
        {
            StopBuildingMusic.OnExit += ToggleDoor;
        }
        private void OnDisable()
        {
            StopBuildingMusic.OnExit -= ToggleDoor;
        }



        public override void Interact()
        {
            if (Input.GetKeyDown(KeyCode.E) && !locked)
            {
                if (!isOpen)
                {
                    //Open Door
                    ToggleDoor();
                    OnEntrance?.Invoke();

                    stopMusic.SetActive(true);
                }
                else
                {
                    ToggleDoor();

                }
            }
        }

        
    }

}