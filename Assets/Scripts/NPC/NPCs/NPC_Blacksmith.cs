using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCspace
{
    public class NPC_Blacksmith : NPC
    {
        public override void OnPlayerEnterRange(Collider Player)
        {
            base.OnPlayerEnterRange(Player);
            Debug.Log(npcName + " says hello");
        }
    }
    

}
