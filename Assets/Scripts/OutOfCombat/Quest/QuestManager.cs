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

        public void SetCurrentQuest(Base_Quest quest)
        {
            Debug.Log(quest.questName);
            activeQuests.Add(quest);
            
        }

        public void SetQuestToQuestLog()
        {
            questNameLog.text = activeQuests[0].questName;
            questDescriptionLog.text = activeQuests[0].questDescription;
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

        public void TEST_SpawnQuestName()
        {
            var questNameUI = Instantiate(questNamePrefab);
            questNameUI.transform.parent = questNameParent.transform;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                TEST_SpawnQuestName();
            }
        }
    }

}