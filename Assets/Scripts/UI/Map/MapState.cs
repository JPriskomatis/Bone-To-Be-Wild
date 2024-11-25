using gameStateSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapState : Singleton<MapState>
{

    public enum StateOfMap {Open, Closed};

    [SerializeField] private GameObject map;
    [SerializeField] private StateOfMap currentState;

    public void SetState(StateOfMap mapState)
    {
        
        if (mapState == StateOfMap.Open)
        {
            OpenState();
        } else
        {
            CloseState();
        }
    }

    public void OpenState()
    {
        
        GameStatController.Instance.SetState(GameStatController.CurrentGameState.CompletePause);
        map.SetActive(true);
        currentState = StateOfMap.Open;
    }
    public void CloseState()
    {
        GameStatController.Instance.SetState(GameStatController.CurrentGameState.Resume);
        map.SetActive(false);
        currentState = StateOfMap.Closed;
    }
}
