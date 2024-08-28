using Interaction;
using questSpace;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class NPC_Barman : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TextAppear.RemoveText();
            GetComponent<Quest_Sample>().SetUI();
        }
    }

    public void OnInteractEnter()
    {
        TextAppear.SetText("Talk");
    }

    public void OnInteractExit()
    {
        TextAppear.RemoveText();
    }
}
