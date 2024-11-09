using Codice.Client.BaseCommands;
using gameStateSpace;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace questSpace
{
    public class QuestManager : MonoBehaviour
    {
        public static event Action<bool> OnQuestOpen;

        public static QuestManager Instance;
        
        public List<Base_Quest> activeQuests = new List<Base_Quest>();


        [Header("UI Elements")]
        [SerializeField] private GameObject questPanel;
        [SerializeField] private TextMeshProUGUI questTitle;
        [SerializeField] private Image questIcon;
        [SerializeField] private TextMeshProUGUI questDescription;
        [SerializeField] private TextMeshProUGUI questCurrencyReward;
        //[SerializeField] private TextMeshProUGUI questReward;

        [Header("Quest Log UI")]
        [SerializeField] private GameObject questLog;
        [SerializeField] private TextMeshProUGUI questNameLog;
        [SerializeField] private TextMeshProUGUI questDescriptionLog;
        [SerializeField] private GameObject questNamePrefab;
        [SerializeField] private GameObject questNameParent;




        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        //Reset Mouse visibility;
        public void ResetMouse()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
            GameStatController.Instance.SetState(GameStatController.CurrentGameState.Resume);
        }

        //Open and Close Quest Tab;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                if(questLog.activeInHierarchy)
                {
                    DisableQuestLogPanel();
                }
                else
                {
                    ActivateQuestLogPanel();
                }
            }
        }
        public void ActivateQuestLogPanel()
        {
            questLog.SetActive(true);
            Cursor.visible = true;
            GameStatController.Instance.SetState(GameStatController.CurrentGameState.Paused);

        }
        public void DisableQuestLogPanel()
        {
            questLog.SetActive(false);
            Cursor.visible = false;
            GameStatController.Instance.SetState(GameStatController.CurrentGameState.Resume);
        }
        public void SetCurrentQuest(Base_Quest quest)
        {
            Debug.Log(quest.questName);
            activeQuests.Add(quest);
            
        }

        public void SetQuestToQuestLog()
        {
            
            InstantiateQuestName();

            //Find the last questName Prefab;
            Transform lastQuestNamePrefab = questNameParent.transform.GetChild(questNameParent.transform.childCount - 1);

            TextMeshProUGUI lastQuestName =  lastQuestNamePrefab.Find("NameOfQuest").GetComponent<TextMeshProUGUI>();
            lastQuestName.text = activeQuests[activeQuests.Count-1].questName;
            //questNameLog.text = activeQuests[0].questName;
            //questDescriptionLog.text = activeQuests[0].questDescription;
        }

        public void RevealQuestDescription(string nameOfQuest)
        {
            //Find the Correct quest;
            for(int i=0; i<activeQuests.Count; i++)
            {
                Debug.Log("Active Quest Name: " + activeQuests[i].questName);
                Debug.Log("Name Of Quest " + nameOfQuest);
                if (nameOfQuest.Equals(activeQuests[i].questName)){
                    //Reveal the quest description;
                    questDescriptionLog.text = activeQuests[i].questDescription;
                    Debug.Log("Yes");
                }
            }


        }


        public void RemoveCurrentQuest()
        {
            activeQuests.RemoveAt(activeQuests.Count-1);
        }

        public void SetUIQuest(Base_Quest quest)
        {



            OnQuestOpen?.Invoke(true);

            questPanel.SetActive(true);
            questTitle.text = quest.questName;
            questDescription.text = quest.questDescription;
            questIcon.sprite = quest.questIcon;
            questCurrencyReward.text = quest.questCurrencyReward.ToString();
            //questReward.text = quest.xpReward.ToString();

            //Activate cursor;
            Cursor.visible = true;

            // Lock the cursor to the center of the screen
            Cursor.lockState = CursorLockMode.None;

            //Freeze player Camera;
            GameStatController.Instance.SetState(GameStatController.CurrentGameState.Paused);
            
        }

        public void RemoveUI()
        {
            questPanel.SetActive(false);
            OnQuestOpen?.Invoke(false);

            //Enable mouse;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;

            GameStatController.Instance.SetState(GameStatController.CurrentGameState.Resume);
        }

        public void InstantiateQuestName()
        {
            //This creates a new questName placeholder;
            var questNameUI = Instantiate(questNamePrefab);
            questNameUI.transform.parent = questNameParent.transform;
        }
    }

}