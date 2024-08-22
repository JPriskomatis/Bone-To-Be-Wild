using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCspace
{
    public class NPC_Blacksmith : Base_NPC
    {



        [SerializeField] private Animator anim;

        
        public override void OnPlayerEnterRange(Collider Player)
        {

            base.OnPlayerEnterRange(Player);
            //We want to play this animation only for the blacksmith;
            PointAnimation();

        }

        private void PointAnimation()
        {
            anim.SetTrigger("point");
        }
    }
    

}
