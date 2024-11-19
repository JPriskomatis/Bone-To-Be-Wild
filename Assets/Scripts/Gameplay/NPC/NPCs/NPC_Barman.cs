using Dialoguespace;
using gameStateSpace;
using Interaction;
using questSpace;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Barman : HasDialogue
{
    public static event Action OnGazeQuestActivation;
    public static event Action OnEnableBenjamin;
    private Action GazeQuestDelegate;
    

    [SerializeField] private Button acceptButton;

    private void Start()
    {
        acceptButton.onClick.AddListener(delegate() { NextDialogue(); });
        acceptButton.onClick.AddListener(delegate() {  EnableBenjaminDialogue(); });
    }
    public void NextDialogue()
    {
        Debug.Log("Clicked accept!");
        selectedInkJSON++;
    }

    public void EnableBenjaminDialogue()
    {
        Debug.Log("Invoked benjamin");
        OnEnableBenjamin?.Invoke();
    }
    protected override Dictionary<string, Action> GetExternalFunctions()
    {
        GazeQuestDelegate = ActivateQuestPanel;

        var externalFunctions = new Dictionary<string, Action>();

        if(GazeQuestDelegate != null)
        {
            externalFunctions.Add("ActivateQuestPanel", GazeQuestDelegate);
        }
        else
        {
            Debug.LogWarning("ActivateQuestPanel function is null");
        }

        return externalFunctions;
    }
    private void ActivateQuestPanel()
    {
        GetComponent<Quest_Sample>().SetUI();
        StartCoroutine(ShowMouse());

    }

    IEnumerator ShowMouse()
    {
        yield return new WaitForSeconds(1f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
