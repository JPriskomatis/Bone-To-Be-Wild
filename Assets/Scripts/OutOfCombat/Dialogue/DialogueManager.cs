using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Dialoguespace
{
    public class DialogueManager : MonoBehaviour
    {
        [Header("Dialogue UI")]
         
        [SerializeField] private GameObject dialogueCanvas;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private Image npcPortrait;

        [Header("Choices UI")]
        [SerializeField] private GameObject[] choices;
        private TextMeshProUGUI[] choicesText;


        public Story currentStory;

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

        public void EnterDialogueMode(Sprite portrait, TextAsset inkJSON, Dictionary<string, System.Action> externalFunctionsDictionary = null)
        {
            npcPortrait.sprite = portrait;
            Cursor.lockState = CursorLockMode.None;  // Unlock the cursor
            Cursor.visible = true;

            currentStory = new Story(inkJSON.text);

            // Check if the dictionary is provided, and bind functions if it is
            if (externalFunctionsDictionary != null)
            {
                foreach (var function in externalFunctionsDictionary)
                {
                    currentStory.BindExternalFunction(function.Key, function.Value);
                }
            }


            dialogueIsPlaying = true;
            dialogueCanvas.SetActive(true);
            ContinueStory();
        }

        private void ContinueStory()
        {
            if (currentStory.canContinue)
            {
                string nextText = currentStory.Continue();

                // Check if the next text is empty or just whitespace
                if (!string.IsNullOrWhiteSpace(nextText))
                {
                    dialogueText.text = nextText;
                    DisplayChoices();
                }
                else
                {
                    StartCoroutine(ExitDialogueMode());
                }
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

            try
            {
                // Attempt to unbind the function
                currentStory.UnbindExternalFunction("OpenGate");
            }
            catch (Exception ex) // Catch specific exceptions if you know what to expect
            {
                Debug.LogWarning("Failed to unbind OpenGate function: " + ex.Message);
            }

            // Hide cursor again
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
            foreach (Choice choice in currentChoices)
            {
                Debug.Log($"Displaying choice {index}: {choice.text}"); // Log choice info
                choices[index].gameObject.SetActive(true);
                choicesText[index].text = choice.text;
                index++;
            }

            // Disable remaining unused choice objects
            for (int i = index; i < choices.Length; i++)
            {
                choices[i].gameObject.SetActive(false);
            }

            choicesActive = currentChoices.Count > 0;
        }

        public void MakeChoice(int choiceIndex)
        {
            if (choiceIndex >= 0 && choiceIndex < currentStory.currentChoices.Count)
            {
                Debug.Log($"Making choice {choiceIndex}: {currentStory.currentChoices[choiceIndex].text}"); // Debug log
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



