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
        public Base_Quest currentQuest;


        //UI Elements;
        [SerializeField] private GameObject questPanel;
        [SerializeField] private TextMeshProUGUI questTitle;
        [SerializeField] private Image questIcon;
        [SerializeField] private TextMeshProUGUI questDescription;
        [SerializeField] private TextMeshProUGUI questCurrencyReward;
        //[SerializeField] private TextMeshProUGUI questReward;


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
    }

}