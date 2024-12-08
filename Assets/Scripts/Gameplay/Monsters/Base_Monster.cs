using combat;
using Damageables;
using System.Collections;
using UI;
using UnityEngine;

namespace monster
{
    public class Base_Monster : MonoBehaviour, ISwordDamageable, ISpellDamageable, ICombat
    {
        [Header("Monster Stats")]
        public int maxHealth;
        public int currentHealth;
        public int moveSpeed;
        public int damage;
        [SerializeField] protected float attackRadius;

        [Header("Components")]
        public Animator anim;
        public SphereCollider attackCollider;

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
            currentHealth = maxHealth;
        }

        // TESTING PURPOSES ONLY
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.G))
            {
                TransitionToState(MonsterState.Hurt);
                currentHealth -= 15;
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
            //Update the health slider;
            GetComponent<Enemy_UI>().UpdateSlider(currentHealth, maxHealth);
            if(currentHealth <= 0)
            {
                TransitionToState(MonsterState.Death);
            }
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

        protected virtual void DeathState()
        {
            //PerformAttack
            Debug.Log("Death");
        }
        #endregion

        #region Combat Functions


        public void SpellDamageable()
        {
            throw new System.NotImplementedException();
        }

        public void SwordDamageable(int damage)
        {
            Debug.Log("Got Hit");
            //Enter Hurt state;
            TakeDamage(damage);
            TransitionToState(MonsterState.Hurt);
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
        }

        #endregion

        #region Environement Associated Functinos
        public void CloseToPlayer(GameObject player)
        {
            if(currentState!= MonsterState.Death){
                Debug.Log("Player is within range");

                StartCoroutine(LookTowardsPlayer(player, 2f));
                if (!inAttack)
                {
                    StartCoroutine(MoveTowardsPlayer(player));
                }
            }
            

        }

        IEnumerator LookTowardsPlayer(GameObject player, float rotationSpeed)
        {
            
            playerToFollow = player;
            // Calculate the direction to the player
            Vector3 relativePos = player.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(relativePos, Vector3.up);

            // Smoothly rotate towards the target rotation
            while (!(currentState==MonsterState.Death))
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
            
            
            while (Vector3.Distance(transform.position, player.transform.position) > attackRadius && !inAttack)
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
        //We call this function when the monster attacks;
        protected void SetAttackCollider()
        {
            attackCollider.enabled = !attackCollider.enabled;
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player got damaged");
                other.GetComponent<ICombat>().TakeDamage(damage);
                SetAttackCollider();
            }
        }
        protected void DisableAllComponents()
        {
            Component[] components = this.GetComponents<Component>();

            foreach (Component component in components)
            {
                // Check if the component is not an Animator or Transform
                if (!(component is Animator) && !(component is Transform))
                {
                    // Disable the component if it has an 'enabled' property
                    if (component is Behaviour behaviourComponent)
                    {
                        //behaviourComponent.enabled = false;
                        Destroy(behaviourComponent);
                    }
                }
            }
        }
        #endregion
    }

}