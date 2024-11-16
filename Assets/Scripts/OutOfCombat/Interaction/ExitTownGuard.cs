using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dialoguespace
{
    public class ExitTownGuard : HasDialogue
    {
        public static event Action OnLeaveTown;

        // Declare a delegate for OpenGate
        private Action leaveTownDelegate;

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
            OnLeaveTown?.Invoke();
        }
    }
}
