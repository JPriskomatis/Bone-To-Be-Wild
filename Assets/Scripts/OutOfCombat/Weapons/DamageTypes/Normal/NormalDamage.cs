using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSpace
{
    public class NormalDamage : IDoDamage
    {
        public void DoDamage(int damage)
        {
            Debug.Log("Deal: " + damage);
        }
    }

}
