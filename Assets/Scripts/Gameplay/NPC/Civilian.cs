using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCspace
{
    public abstract class Civilian : MonoBehaviour
    {
        protected enum AnimationState
        {
            Talking,
            Laugh,
            Drinking
        }
        public string npcName;

        protected Animator anim;
        private AnimationState currentState;

        private void Start()
        {
            anim = GetComponent<Animator>();

            InitializeAnimator();

            currentState = AnimationState.Talking;
            PlayAnimation(currentState);
        }

        protected abstract void InitializeAnimator();

        protected void PlayAnimation(AnimationState state)
        {
            // Set the parameter for transitioning
            anim.SetInteger("AnimationState", (int)state);
            currentState = state;
        }

        // Method to cycle through animations
        public void CycleAnimation()
        {
            // Move to the next animation state
            currentState = (AnimationState)(((int)currentState + 1) % 3);
            PlayAnimation(currentState);
        }
        public void OnAnimationEnd()
        {
            CycleAnimation();
        }
    }

}
