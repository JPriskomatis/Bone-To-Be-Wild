using Damageables;
using PlayerSpace;
using System;
using System.Collections;
using UnityEngine;

namespace NPCspace
{
    public abstract class Base_Enemy : MonoBehaviour, ISpellDamageable, ISwordDamageable
    {
        public static event Action OnDeath;
        protected enum AnimationState
        {
            Idle,
            Running,
            Hurt,
            Combat
        }

        protected AnimationState currentState;

        public string npcName;
        public int health;
        public Animator anim;

        [Header("Enemy Stats")]
        public float speed;
        public float attackSpeed;

        public bool canAttack;
        public abstract void CloseToPlayer(GameObject player);
        protected abstract void InitializeAnimator();

        private void Start()
        {
            InitializeAnimator();
        }
        protected virtual void PlayAnimation(AnimationState state)
        {
            // Set the parameter for transitioning;
            anim.SetInteger("AnimationState", (int)state);
            currentState = state;
        }

        // Method to cycle through animations
        public void CycleAnimation()
        {
            // Move to the next animation state
            currentState = (AnimationState)(((int)currentState + 1) % 2);
            PlayAnimation(currentState);
        }
        public void OnAnimationEnd()
        {
            CycleAnimation();
        }

        public void SpellDamageable()
        {
            Debug.Log(transform.root.gameObject.name + " was hurt!");
            StartCoroutine(DelayedDeathAnima());
        }
        IEnumerator DelayedDeathAnima()
        {
            yield return new WaitForSeconds(1f);
            Death();
        }

        public void Death()
        {
            GetComponentInParent<AudioSource>().Play();
            anim.SetTrigger("death");
            OnDeath?.Invoke();

        }

        public virtual void SwordDamageable()
        {
            Debug.Log("SwordDamageable");
            //PlayAnimation(AnimationState.Hurt);
            //Perform Hit or Death;
            //Death();
        }
    }

}
