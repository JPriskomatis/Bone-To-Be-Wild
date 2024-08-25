using Interaction;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Buildings
{
    public class BountyBoard : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TextAppear.SetText("You are reading the Bounty Board");
            }
        }

        public void OnInteractEnter()
        {
            TextAppear.SetText("Read Bounty Board");
        }

        public void OnInteractExit()
        {
            throw new System.NotImplementedException();
        }
    }

}