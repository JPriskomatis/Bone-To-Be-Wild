using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCspace
{

    public class Tavern_Civilain : Base_TavernCivilian
    {
        [SerializeField]
        AnimationState startingState;
        protected override void InitializeAnimator()
        {
            currentState = startingState;
            PlayAnimation(currentState);
        }

    }

}