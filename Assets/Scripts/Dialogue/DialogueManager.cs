using Ink.Runtime;
using NPCspace;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Dialoguespace
{
    public class DialogueManager : MonoBehaviour
    {
        [Header("Dialogue UI")]
        
        [SerializeField] private GameObject dialogueCanvas;
        
        [SerializeField] private TextMeshProUGUI dialogueText;

        private Story currentStory;

        public bool dialogueIsPlaying { get; private set; }

        


        private static DialogueManager instance;

        private void Awake()
        {
            instance = this;
            if(instance != null )
            {
                Debug.LogWarning("Error, found more than one Dialogue Managers");
            }
        }

        private void Start()
        {
            dialogueIsPlaying = false;
            dialogueCanvas.SetActive(false);
        }

        private void Update()
        {
            if (!dialogueIsPlaying)
            {
                return;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ContinueStory();
                }
            }
        }

        public static DialogueManager GetInstance()
        {
            return instance;
        }

        public void EnterDialogueMode(TextAsset inkJSON)
        {
            currentStory = new Story(inkJSON.text);

            dialogueIsPlaying = true;

            dialogueCanvas.SetActive(true);
            ContinueStory();
        }

        private void ContinueStory()
        {
            if (currentStory.canContinue)
            {
                dialogueText.text = currentStory.Continue();
            }
            else
            {
                StartCoroutine(ExitDialogueMode());
            }
        }

        private IEnumerator ExitDialogueMode()
        {
            yield return new WaitForSeconds(0.2f);

            dialogueIsPlaying = false;
            dialogueCanvas.SetActive(false);
            dialogueText.text = "";
        }

    }

}
