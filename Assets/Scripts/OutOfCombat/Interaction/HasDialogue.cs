using Interaction;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Dialoguespace
{
    public abstract class HasDialogue : MonoBehaviour, IInteractable
    {
        [Header("Dialogue Elements")]
        [SerializeField] private Sprite potrait;
        [SerializeField] protected TextAsset inkJSON;

        public virtual void Interact()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                var externalFunctionsDictionary = GetExternalFunctions();
                DialogueManager.GetInstance().EnterDialogueMode(potrait, inkJSON, externalFunctionsDictionary);
            }
        }

        protected virtual Dictionary<string, System.Action> GetExternalFunctions()
        {
            return new Dictionary<string, System.Action>();
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
