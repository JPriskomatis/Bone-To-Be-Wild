using Interaction;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Buildings
{

    public class Note_BountyBoard : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                TextAppear.SetText("This is "+this.gameObject.name);
            }
        }

        public void OnInteractEnter()
        {
            TextAppear.SetText("Read Note");
        }

        public void OnInteractExit()
        {
            TextAppear.RemoveText();
        }
    }

}