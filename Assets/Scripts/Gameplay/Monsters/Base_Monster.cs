using System.Collections;
using UnityEngine;

namespace monster
{
    public class Base_Monster : MonoBehaviour
    {
        [Header("Monster Stats")]
        public int maxHealth;
        public int currentHealth;
        public int moveSpeed;
        public int damage;

        [Header("Components")]
        public Animator anim;

        private GameObject playerToFollow;
        private bool combatRange;
        protected bool inAttack;


        //States;
        public enum MonsterState { Idle, Combat, Running, Hurt, Death};
        [Header("State")]
        public MonsterState currentState;
        public MonsterState startingState;


        private void Start()
        {
            currentState = startingState;
        }

        // TESTING PURPOSES ONLY
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.G))
            {
                TransitionToState(MonsterState.Hurt);
            }
        }
        #region States

        protected MonsterState TransitionToState(MonsterState state)
        {

            ExitState(state);
            currentState = state;
            EnterState(state);
            return currentState;
        }
        protected virtual void ExitState(MonsterState newState)
        {

        }


        protected void EnterState(MonsterState state)
        {
            //TODO:
            //implement a smooth transition to Running state;
            switch (state)
            {
                case MonsterState.Idle:
                    IdleState();
                    break;
                case MonsterState.Hurt:
                    HurtState();
                    break;
                case MonsterState.Running:
                    RunningState();
                    break;
                case MonsterState.Combat:
                    CombatState();
                    break;
                case MonsterState.Death:
                    DeathState();
                    break;
            }
        }
        protected virtual void HurtState()
        {
            //PerformAttack
            Debug.Log("Hurt");
        }
        protected virtual void IdleState()
        {
            //PerformAttack
            Debug.Log("Idle");
        }
        protected virtual void RunningState()
        {
            //PerformAttack
            Debug.Log("Running");
            anim.SetFloat("Locomotion", moveSpeed);
        }
        protected virtual void CombatState()
        {
            //PerformAttack
            Debug.Log("Combat");
            
        }
        #endregion

        #region Combat Functions
        protected virtual void DeathState()
        {
            //PerformAttack
            Debug.Log("Death");
        }
        #endregion

        #region Environement Associated Functinos
        public void CloseToPlayer(GameObject player)
        {
            Debug.Log("Player is within range");

            StartCoroutine(LookTowardsPlayer(player, 10f));
            if (!inAttack)
            {
                StartCoroutine(MoveTowardsPlayer(player));
            }
        }

        IEnumerator LookTowardsPlayer(GameObject player, float rotationSpeed)
        {
            playerToFollow = player;
            // Calculate the direction to the player
            Vector3 relativePos = player.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(relativePos, Vector3.up);

            // Smoothly rotate towards the target rotation
            while (true)
            {
                // Update the direction to the player
                relativePos = player.transform.position - transform.position;
                targetRotation = Quaternion.LookRotation(relativePos, Vector3.up);

                // Interpolate towards the target rotation
                transform.rotation = Quaternion.Lerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );

                // Wait until the next frame before continuing the loop
                yield return null;
            }
        }

        IEnumerator MoveTowardsPlayer(GameObject player)
        {
            
            
            while (Vector3.Distance(transform.position, player.transform.position) > 3f && !inAttack)
            {
                
                TransitionToState(MonsterState.Running);
                // Calculate the step to move towards the target
                float step = moveSpeed * Time.deltaTime;

                // Move the object towards the target
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);

                // Wait until the next frame before continuing the loop
                yield return null;
            }
            
            Debug.Log("Monster reached the player.");

            
            TransitionToState(MonsterState.Combat);

            

        }
        #endregion

        #region Helper Functions
        //private IEnumerator SmoothlyTransitionLocomotionToZero(float duration)
        //{
        //    float elapsed = 0f;
        //    float initialLocomotion = anim.GetFloat("Locomotion");

        //    while (elapsed < duration)
        //    {
        //        elapsed += Time.deltaTime;
        //        float newLocomotionValue = Mathf.Lerp(initialLocomotion, 0f, elapsed / duration);
        //        anim.SetFloat("Locomotion", newLocomotionValue);
        //        yield return null;
        //    }

        //    anim.SetFloat("Locomotion", 0f);
        //}
        #endregion
    }

}