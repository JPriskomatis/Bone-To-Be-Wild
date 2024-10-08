using UnityEngine;

namespace stateMachine
{

    public class StateMachine : MonoBehaviour
    {
        public State CurrentState { get; private set; }

        private void Update()
        {
            if (CurrentState != null)
            {
                CurrentState.LogicUpdate();
            }
        }

        private void FixedUpdate()
        {
            if (CurrentState != null)
            {
                CurrentState.PhysicsUpdate();
            }
        }

        public void ChangeState(State newState)
        {
            if (CurrentState != null)
            {
                CurrentState.Exit();
            }

            CurrentState = newState;

            if (CurrentState != null)
            {
                CurrentState.Enter();
            }
        }
    }

}