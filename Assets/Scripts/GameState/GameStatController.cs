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

        //Helper method
        public void SetToResumeState()
        {
            SetState(CurrentGameState.Resume);
        }
        public void SetState(CurrentGameState state)
        {
            currentState = state;
            if(state == CurrentGameState.Paused)
            {
                //OnPause?.Invoke(true);
                PauseGame();
            } else
            {
                //OnPause?.Invoke(false);
                ResumeGame();
            }
                
        }

        public CurrentGameState GetState()
        {
            return currentState;
        }

        public void PauseGame()
        {
            OnPause?.Invoke(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //SetState(CurrentGameState.Paused);
        }

        public void ResumeGame()
        {
            OnPause?.Invoke(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //SetState(CurrentGameState.Resume);
        }
    }

}