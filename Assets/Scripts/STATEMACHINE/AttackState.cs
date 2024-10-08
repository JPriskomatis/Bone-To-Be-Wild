using stateMachine;
using UnityEngine;

namespace stateMachine
{

    public class AttackState : State
    {
        private float attackDuration = 1f;
        private float elapsedTime;

        public AttackState(StateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            Debug.Log("Entering Attack State");
            elapsedTime = 0f;
        }

        public override void LogicUpdate()
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= attackDuration)
            {
                stateMachine.ChangeState(new IdleState(stateMachine));
            }
        }

        public override void Exit()
        {
            Debug.Log("Exiting Attack State");
        }
    }

}