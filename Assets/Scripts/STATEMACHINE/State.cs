

namespace stateMachine
{

    public abstract class State
    {
        protected StateMachine stateMachine;

        public State(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public virtual void Enter() { } // Called when entering the state
        public virtual void LogicUpdate() { } // Called every frame
        public virtual void PhysicsUpdate() { } // Called in FixedUpdate
        public virtual void Exit() { } // Called when exiting the state
    }

}