using Dialoguespace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCspace
{
    public class NPC_ArenaGuard : HasDialogue
    {
        public static event Action OnEnterArena;

        private Action EnterArenaDelegate;

        protected override Dictionary<string, System.Action> GetExternalFunctions()
        {
            // Initialize the delegate with the OpenGate method
            EnterArenaDelegate = EnterArena;

            var externalFunctions = new Dictionary<string, System.Action>();

            // Check if the delegate is not null before adding it
            if (EnterArenaDelegate != null)
            {
                externalFunctions.Add("EnterArena", EnterArenaDelegate);
            }
            else
            {
                Debug.LogWarning("EnterArena function is null and cannot be added to external functions.");
            }

            return externalFunctions;
        }

        public void EnterArena()
        {
            Debug.Log("Entering the arena");
        }
    }

}