using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCspace
{

    public class Tavern_Civilain : Base_TavernCivilian
    {
        [SerializeField]
        AnimationState startingState;

        [SerializeField] private GameObject beerMug;

        public CivilainState npcState;
        public enum CivilainState
        {
            Company,
            Alone,
            Drinking,
            All
        }


        private void DrinkBeer()
        {
            beerMug.SetActive(true);
        }
        public void RemoveBeer()
        {
            beerMug.SetActive(false);
        }



        protected override void PlayAnimation(AnimationState state)
        {
            // Set the parameter for transitioning;
            anim.SetInteger("AnimationState", (int)state);
            currentState = state;

            if (this.currentState == AnimationState.Drinking)
            {
                DrinkBeer();

            }

            //Based on
            if(npcState == CivilainState.Alone)
            {
                if(this.currentState == AnimationState.Talking || this.currentState == AnimationState.Laugh)
                {
                    CycleAnimation();
                }
            }
            if(npcState == CivilainState.Drinking)
            {
                if (this.currentState == AnimationState.Talking)
                {
                    CycleAnimation();
                }
            }
        }
        

        protected override void InitializeAnimator()
        {
            currentState = startingState;
            
            PlayAnimation(currentState);
        }


    }

}