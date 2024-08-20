using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCspace
{

    public class Tavern_Civilain : Civilian
    {
        protected override void InitializeAnimator()
        {
            anim.Play("Sitting_Talking");
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CycleAnimation();
            }
        }
    }

}