using UnityEngine;

namespace stateMachine
{

    public class IdleState : State
    {
        public IdleState(StateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            Debug.Log("Entering Idle State");
        }

        public override void LogicUpdate()
        {
            // Check for transition conditions
            if (Input.GetKey(KeyCode.W))
            {
                
                stateMachine.ChangeState(new WalkState(stateMachine));
            }
            else if (Input.GetKeyDown(KeyCode.Space)) // Attack on space bar
            {
                stateMachine.ChangeState(new AttackState(stateMachine));
            }
        }

        public override void Exit()
        {
            Debug.Log("Exiting Idle State");
        }
    }

}