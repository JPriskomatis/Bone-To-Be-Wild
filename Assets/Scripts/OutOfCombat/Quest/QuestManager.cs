using Codice.Client.BaseCommands;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace questSpace
{
    public class QuestManager : MonoBehaviour
    {
        public static event Action<bool> OnQuestOpen;

        public static QuestManager Instance;
        public Base_Quest currentQuest;


        //UI Elements;
        [SerializeField] private GameObject questPanel;
        [SerializeField] private TextMeshProUGUI questTitle;
        [SerializeField] private TextMeshProUGUI questInfo;
        [SerializeField] private TextMeshProUGUI questReward;


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
            currentQuest = quest;
        }

        public Base_Quest GetCurrentQuest()
        {
            return currentQuest;
        }

        public void RemoveCurrentQuest()
        {
            currentQuest = null;
        }

        public void SetUIQuest(Base_Quest quest)
        {

            OnQuestOpen?.Invoke(true);

            questPanel.SetActive(true);
            questTitle.text = quest.questName;
            questInfo.text = quest.questDescription;
            questReward.text = quest.xpReward.ToString();

            //Activate cursor;
            Cursor.visible = true;

            // Lock the cursor to the center of the screen
            Cursor.lockState = CursorLockMode.None;

            //Freeze player Camera;
            
        }

        public void RemoveUI()
        {
            questPanel.SetActive(false);
            OnQuestOpen?.Invoke(false);

            //Enable mouse;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

}