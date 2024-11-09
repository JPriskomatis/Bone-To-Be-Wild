using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace questSpace
{
    public class QuestName : MonoBehaviour
    {
        public TextMeshProUGUI questName;
        public void CheckQuestName()
        {
            QuestManager.Instance.RevealQuestDescription(questName.text);
        }
    }

}