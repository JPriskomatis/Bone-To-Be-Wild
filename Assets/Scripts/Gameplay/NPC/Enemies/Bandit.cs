using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCspace
{
    /// <summary>
    /// All bandits should have an animation called "death" as we use this specific one from the Base_Enemy;
    /// </summary>
    /// 
    public class Bandit : Base_Enemy
    {
        public override void CloseToPlayer()
        {
            Debug.Log("Player is here");
        }
    }

}