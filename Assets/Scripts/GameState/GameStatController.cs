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

        [Header("Menu Settings")]
        [SerializeField] private GameObject menuPanel;

        public enum CurrentGameState
        {
            Paused,
            CompletePause,
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
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (menuPanel.activeInHierarchy)
                {
                    SetState(CurrentGameState.Resume);
                    menuPanel.SetActive(false);
                }
                else
                {
                    SetState(CurrentGameState.CompletePause);
                    menuPanel.SetActive(true);
                }
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
            }
            else if (state == CurrentGameState.CompletePause)
            {
                //OnPause?.Invoke(false);
                PauseGame(true);
            }
            else
            {
                ResumeGame();
            }
                
        }

        public CurrentGameState GetState()
        {
            return currentState;
        }

        public void PauseGame(bool completePause=false)
        {
            OnPause?.Invoke(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //SetState(CurrentGameState.Paused);
            if (completePause)
            {
                Time.timeScale = 0;
            }
            
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
            OnPause?.Invoke(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //SetState(CurrentGameState.Resume);
        }
    }

}