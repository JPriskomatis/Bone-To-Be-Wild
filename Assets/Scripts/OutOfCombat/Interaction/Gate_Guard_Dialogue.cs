using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dialoguespace
{
    public class Gate_Guard_Dialogue : HasDialogue
    {
        public static event Action OnGateOpen;
        protected override Dictionary<string, System.Action> GetExternalFunctions()
        {
            return new Dictionary<string, System.Action>
            {
                { "OpenGate", OpenGate }
            };
        }

        private void OpenGate()
        {
            Debug.Log("OpenGate function called.");
            OnGateOpen?.Invoke();
        }
    }
}
