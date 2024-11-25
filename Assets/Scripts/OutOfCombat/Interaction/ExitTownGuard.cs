using Codice.Client.BaseCommands;
using gameStateSpace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialoguespace
{
    public class ExitTownGuard : HasDialogue
    {

        // Declare a delegate for OpenGate
        private Action leaveTownDelegate;
        [Header("Map UI")]
        [SerializeField] private GameObject map;

        protected override Dictionary<string, System.Action> GetExternalFunctions()
        {
            // Initialize the delegate with the OpenGate method
            leaveTownDelegate = LeaveTown;

            var externalFunctions = new Dictionary<string, System.Action>();

            // Check if the delegate is not null before adding it
            if (leaveTownDelegate != null)
            {
                externalFunctions.Add("LeaveTown", leaveTownDelegate);
            }
            else
            {
                Debug.LogWarning("LeaveTown function is null and cannot be added to external functions.");
            }

            return externalFunctions;
        }

        private void LeaveTown()
        {
            Debug.Log("ExitTownGuard function called.");
            
            //Exit the dialogue;
            StartCoroutine(DialogueManager.GetInstance().ExitDialogueMode());

            //Open the Map;
            StartCoroutine(OpenMap());
            
            
            //SceneTransition.Instance.GoToScene(ConstantValues.CAVE_SCENE);
        }
        IEnumerator OpenMap()
        {
            yield return new WaitForSeconds(0.3f);
            
            MapState.Instance.SetState(MapState.StateOfMap.Open);
            
        }
    }
}
