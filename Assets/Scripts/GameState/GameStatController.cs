using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameStateSpace
{
    public class GameStatController : MonoBehaviour
    {
        public static GameStatController Instance;

        public CurrentGameState currentState;

        public static event Action<bool> OnPause;

        public enum CurrentGameState
        {
            Paused,
            Resume
        }
        private void Start()
        {
            
            currentState = CurrentGameState.Resume; 
        }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);  // Ensures the GameStatController persists across scenes
            }
        }


        public void SetState(CurrentGameState state)
        {
            currentState = state;
            if(state == CurrentGameState.Paused)
            {
                OnPause?.Invoke(true);
            } else
            {
                OnPause?.Invoke(false);
            }
                
        }

        public CurrentGameState GetState()
        {
            return currentState;
        }
    }

}