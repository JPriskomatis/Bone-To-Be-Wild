using stateMachine;
using UnityEngine;

namespace stateMachine
{

    public class WalkState : State
    {
        private float walkSpeed = 2f;

        public WalkState(StateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            Debug.Log("Entering Walk State");
        }

        public override void LogicUpdate()
        {
            // Check for transition conditions
            if (Input.GetKeyUp(KeyCode.W))
            {
                stateMachine.ChangeState(new IdleState(stateMachine));
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                stateMachine.ChangeState(new AttackState(stateMachine));
            }
        }

        public override void PhysicsUpdate()
        {
            // Walking logic
            stateMachine.transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
        }

        public override void Exit()
        {
            Debug.Log("Exiting Walk State");
        }
    }

}