using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dialoguespace
{
    public class Gate_Guard_Dialogue : HasDialogue
    {
        public static event Action OnGateOpen;

        // Declare a delegate for OpenGate
        private Action openGateDelegate;

        protected override Dictionary<string, System.Action> GetExternalFunctions()
        {
            // Initialize the delegate with the OpenGate method
            openGateDelegate = OpenGate;

            var externalFunctions = new Dictionary<string, System.Action>();

            // Check if the delegate is not null before adding it
            if (openGateDelegate != null)
            {
                externalFunctions.Add("OpenGate", openGateDelegate);
            }
            else
            {
                Debug.LogWarning("OpenGate function is null and cannot be added to external functions.");
            }

            return externalFunctions;
        }

        private void OpenGate()
        {
            Debug.Log("OpenGate function called.");
            OnGateOpen?.Invoke();
        }
    }
}
