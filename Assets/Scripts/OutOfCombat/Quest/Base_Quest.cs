using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace questSpace
{
    public abstract class Base_Quest : MonoBehaviour
    {
        public string questName;
        public Sprite questIcon;
        public string questDescription;
        public int questCurrencyReward;
        public int xpReward;

        public void SetUI()
        {
            QuestManager.Instance.SetUIQuest(this);
            SetCurrentQuest();
        }

        public void SetCurrentQuest()
        {
            QuestManager.Instance.SetCurrentQuest(this);
        }

    }

}