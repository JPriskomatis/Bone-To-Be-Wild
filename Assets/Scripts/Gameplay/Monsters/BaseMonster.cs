using Damageables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static monster.Base_Monster;

namespace monster
{
    public class BaseMonster : MonoBehaviour, ISwordDamageable
    {
        [Header("Monster Stats")]
        [SerializeField] protected int currentHealth;
        protected int maxHealth;
        public float moveSpeed;

        [Header("Environement")]
        [SerializeField] protected float chaseDist;
        [SerializeField] protected float attackDist;

        [Header("Components")]
        [SerializeField] protected Animator anim;

        //Attributes;
        [SerializeField] protected float attackSpeed;
        protected bool canAttack = true;
        private WaitForSeconds waitForSeconds;


        //States;
        protected enum State
        {
            Idle,
            Chase,
            Combat,
            Hurt,
            Death
        }

        private State currentState;

        protected void TransitionToState(State newState)
        {
            currentState = newState;
            switch (currentState)
            {
                case State.Idle:
                    IdleState();
                    break;
                case State.Chase:
                    ChaseState();
                    break;
                case State.Combat:
                    CombatState();
                    break;
                case State.Hurt:
                    HurtState();
                    break;
                case State.Death:
                    DeathState();
                    break;
                default:
                    break;
            }
        }
        protected virtual void IdleState()
        {
            Debug.Log("Idle");
        }
        protected virtual void ChaseState()
        {
            Debug.Log("Chase State");
        }
        protected virtual void CombatState()
        {
            Debug.Log("Combat State");
        }
        protected virtual void HurtState()
        {
            Debug.Log("Hurt State");
        }
        protected virtual void DeathState()
        {
            Debug.Log("Death State");
        }

        public void SwordDamageable(int damage)
        {
            currentHealth -= damage;
            if(currentHealth > 0)
            {
                TransitionToState(State.Hurt);
            }
            else
            {
                TransitionToState(State.Death);
            }
        }

        protected IEnumerator StartAttackCooldown()
        {
            yield return new WaitForSeconds(2f);
            canAttack = true;
        }
        public void CloseToPlayer(GameObject player)
        {
            StartCoroutine(MoveAndTransitionCoroutine(player));
        }

        private IEnumerator MoveAndTransitionCoroutine(GameObject player)
        {
            while (true)
            {
                transform.LookAt(player.transform.position);
                Debug.Log(Vector3.Distance(transform.position, player.transform.position));

                if (Vector3.Distance(transform.position, player.transform.position) <= attackDist)
                {
                    TransitionToState(State.Combat);
                } else
                {
                    if (Vector3.Distance(transform.position, player.transform.position) <= chaseDist)
                    {
                        transform.position += transform.forward * moveSpeed * Time.deltaTime;
                        TransitionToState(State.Chase);
                    }
                }
                

                yield return null;
            }
        }
    }

}