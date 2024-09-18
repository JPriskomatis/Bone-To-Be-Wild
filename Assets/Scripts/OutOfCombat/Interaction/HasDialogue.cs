using Dialoguespace;
using Interaction;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Dialoguespace
{

    public class HasDialogue : MonoBehaviour, IInteractable
    {
        [SerializeField] string npcName;
        [SerializeField] private TextAsset inkJSON;

        public void Interact()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }

        public void OnInteractEnter()
        {
            TextAppear.SetText("Talk");
        }

        public void OnInteractExit()
        {
            TextAppear.RemoveText();
        }

    }

}