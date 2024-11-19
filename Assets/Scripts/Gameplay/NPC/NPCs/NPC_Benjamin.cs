using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Benjamin : MonoBehaviour
{
    private void OnEnable()
    {
        NPC_Barman.OnEnableBenjamin += EnableBenjamin;
    }
    private void OnDisable()
    {
        NPC_Barman.OnEnableBenjamin -= EnableBenjamin;
    }

    public void EnableBenjamin()
    {
        this.GetComponent<QuestInfoGiver>().canTalk = true;
    }

}
