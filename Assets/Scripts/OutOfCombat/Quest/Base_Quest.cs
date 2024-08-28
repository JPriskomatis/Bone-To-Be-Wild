using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace questSpace
{
    public abstract class Base_Quest : MonoBehaviour
    {
        public string questName;
        public string questDescription;
        public int xpReward;

        public void SetUI()
        {
            QuestManager.Instance.SetUIQuest(this);
        }

    }

}