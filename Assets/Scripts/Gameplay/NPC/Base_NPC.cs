using Dialoguespace;
using UI;
using UnityEngine;
using Interaction;
using UnityEngine.UI;

namespace NPCspace
{

    /// <summary>
    /// Abstract class that every main NPC will inherit from;
    /// </summary>
    
    public abstract class Base_NPC : MonoBehaviour, IInteractable
    {

        public string npcName;
        public Sprite portrait;

        [Header("Ink JSON")]
        [SerializeField] private TextAsset inkJSON;

        public AudioSource greetingAudio;



        #region INTERACTION INTERFACE
        public void Interact()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TextAppear.SetText("Hellos");

                //Initiate Dialogue;

                DialogueManager.GetInstance().EnterDialogueMode(portrait,inkJSON);
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
            if (greetingAudio != null)
            {
                greetingAudio.Play();
            }
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