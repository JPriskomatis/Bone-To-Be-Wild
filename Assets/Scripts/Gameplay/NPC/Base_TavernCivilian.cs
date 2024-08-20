using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCspace
{
    public abstract class Base_TavernCivilian : MonoBehaviour
    {
        //Animation states for our Tavern NPC;
        //This allow us to easily go from one animation to the other
        //and easily go to a specific animation if we want;
        protected enum AnimationState
        {
            Talking,
            Laugh,
            Drinking
        }
        public string npcName;

        protected Animator anim;
        protected AnimationState currentState;

        private void Start()
        {
            anim = GetComponent<Animator>();

            InitializeAnimator();

        }

        //We override this method in our inheritant class to set
        //the starting animation state;
        protected abstract void InitializeAnimator();

        protected void PlayAnimation(AnimationState state)
        {
            // Set the parameter for transitioning;
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
