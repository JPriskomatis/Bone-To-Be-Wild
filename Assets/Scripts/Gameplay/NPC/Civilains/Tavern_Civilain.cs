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
        }
        

        protected override void InitializeAnimator()
        {
            currentState = startingState;
            
            PlayAnimation(currentState);
        }


    }

}