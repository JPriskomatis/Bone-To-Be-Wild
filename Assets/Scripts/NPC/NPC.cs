using Interaction;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace NPCspace
{

    /// <summary>
    /// Abstract class that every main NPC will inherit from;
    /// </summary>
    
    public abstract class NPC : MonoBehaviour, IInteractable
    {
        public string npcName;


        #region INTERACTION INTERFACE
        public void Interact()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TextAppear.SetText("Hellos");
            }
        }

        public void OnInteractEnter()
        {
            TextAppear.SetText("Talk to "+npcName);
        }

        public void OnInteractExit()
        {
            TextAppear.RemoveText();

        }
        #endregion
    }

}