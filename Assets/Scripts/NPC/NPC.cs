using Dialoguespace;
using Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using Unity.VisualScripting;
using UnityEngine;

namespace NPCspace
{

    /// <summary>
    /// Abstract class that every main NPC will inherit from;
    /// </summary>
    
    public abstract class NPC : MonoBehaviour, IInteractable
    {
        public string npcName;

        [Header("Ink JSON")]
        [SerializeField] private TextAsset inkJSON;

        public AudioSource greetingAudio;



        #region INTERACTION INTERFACE
        public void Interact()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TextAppear.SetText("Hellos");

                //Initiate Dialogue;

                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
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

        #region PLAYER ENTER -> NPC TALK
        public virtual void OnPlayerEnterRange(Collider Player)
        {
            greetingAudio.Play();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnPlayerEnterRange(other);
            }
            
        }
        #endregion
    }

}