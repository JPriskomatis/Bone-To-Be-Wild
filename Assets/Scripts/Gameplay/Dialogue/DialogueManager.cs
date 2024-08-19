using Ink.Runtime;
using NPCspace;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dialoguespace
{
    public class DialogueManager : MonoBehaviour
    {
        [Header("Dialogue UI")]
        
        [SerializeField] private GameObject dialogueCanvas;
        
        [SerializeField] private TextMeshProUGUI dialogueText;

        [Header("Choices UI")]
        [SerializeField] private GameObject[] choices;
        private TextMeshProUGUI[] choicesText;


        private Story currentStory;

        public bool dialogueIsPlaying { get; private set; }

        private static DialogueManager instance;

        private bool choicesActive = false;

        private void Awake()
        {
            // Check if there's already an instance
            if (instance == null)
            {
                // Set the current instance if none exists
                instance = this;
                // Optionally, set this instance to not be destroyed on load
                // DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                // Destroy this instance if another one already exists
                Destroy(gameObject);
            }
        }
    

        private void Start()
        {
            dialogueIsPlaying = false;
            dialogueCanvas.SetActive(false);

            choicesText = new TextMeshProUGUI[choices.Length];

            int index = 0;
            foreach(GameObject choice in choices)
            {
                choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
                index++;
            }
        }

        private void Update()
        {
            if (!dialogueIsPlaying)
            {
                return;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) && !choicesActive)
                {
                    choicesActive = false;
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
            Cursor.lockState = CursorLockMode.None;  // Unlock the cursor
            Cursor.visible = true;

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
                DisplayChoices();
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

            //Hide cursor again
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void DisplayChoices()
        {
            List<Choice> currentChoices = currentStory.currentChoices;

            Debug.Log($"Current choices count: {currentChoices.Count}");


            if (currentChoices.Count > choices.Length)
            {
                Debug.LogWarning("Too many choices");
            }

            int index = 0;
            bool hasActiveChoices = currentChoices.Count > 0;
            foreach (Choice choice in currentChoices)
            {
                choices[index].gameObject.SetActive(true);
                choicesText[index].text = choice.text;
                index++;
            }

            for(int i = index; i < choices.Length; i++)
            {
                choices[i].gameObject.SetActive(false);

            }
            choicesActive = hasActiveChoices;

            //StartCoroutine(SelectFirstChoice());

        }
        public void MakeChoice(int choiceIndex)
        {
            if (choiceIndex >= 0 && choiceIndex < currentStory.currentChoices.Count)
            {
                Debug.Log("Choice Index: "+choiceIndex);
                currentStory.ChooseChoiceIndex(choiceIndex);
                choicesActive = false;
                ContinueStory();
            }
            else
            {
                Debug.LogError($"Choice index {choiceIndex} is out of range. Choices available: {currentStory.currentChoices.Count}");
            }
        }

        //private IEnumerator SelectFirstChoice()
        //{
        //    EventSystem.current.SetSelectedGameObject(null);
        //    yield return new WaitForEndOfFrame();
        //    EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
        //}

    }
}



